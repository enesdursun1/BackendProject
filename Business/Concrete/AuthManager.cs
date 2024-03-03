using Application.Services.Repositories;
using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Dtos.Requests.User;
using Business.Dtos.Responses.Auth;
using Business.Dtos.Responses.User;
using Business.Rules;
using Core.Entities.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using DataAccess.Abstract;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IRefreshTokenDal _refreshTokenRepository;
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly TokenOptions _tokenOptions;
    private readonly AuthBusinessRules _authBusinessRules;

    private readonly IUserOperationClaimDal _userOperationClaimRepository;

    public AuthManager(
        IUserOperationClaimDal userOperationClaimRepository,
        IUserService userService,
        IRefreshTokenDal refreshTokenRepository,
        ITokenHelper tokenHelper,
        IConfiguration configuration,
        IMapper mapper,
        AuthBusinessRules authBusinessRules

    )
    {
        _userOperationClaimRepository = userOperationClaimRepository;
        _userService = userService;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _authBusinessRules = authBusinessRules;
    

        const string tokenOptionsConfigurationSection = "TokenOptions";
        _tokenOptions =
            configuration.GetSection(tokenOptionsConfigurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{tokenOptionsConfigurationSection}\" section cannot found in configuration");
    }

    public async Task<Core.Security.JWT.AccessToken> CreateAccessToken(User user)
    {
        IList<OperationClaim> operationClaims = await _userOperationClaimRepository.GetOperationClaimsByUserIdAsync(user.Id);
        Core.Security.JWT.AccessToken accessToken = _tokenHelper.CreateAccessToken(user, operationClaims);
        return accessToken;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(int userId)
    {
        //List<RefreshToken> refreshTokens = await _refreshTokenRepository.GetOldRefreshTokensAsync(userId, _tokenOptions.RefreshTokenTTL);
        //await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);
    }

    public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == token);
        return refreshToken;
    }

    public async Task RevokeRefreshToken(RefreshToken refreshToken, string ipAddress, string? reason = null, string? replacedByToken = null)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = reason;
        refreshToken.ReplacedByToken = replacedByToken;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
        RefreshToken newRefreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        await RevokeRefreshToken(refreshToken, ipAddress, reason: "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    public async Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
    {
        RefreshToken? childToken = await _refreshTokenRepository.GetAsync(predicate: r => r.Token == refreshToken.ReplacedByToken);

        if (childToken?.Revoked != null && childToken.Expires <= DateTime.UtcNow)
            await RevokeRefreshToken(childToken, ipAddress, reason);
        else
            await RevokeDescendantRefreshTokens(refreshToken: childToken!, ipAddress, reason);
    }

    public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);
        return Task.FromResult(refreshToken);
    }

    public async Task<LoggedResponse> Login(UserForLoginDto userForLoginDto, string ipAddress)
    {
        User? user = await _userService.GetByEmailAsync(userForLoginDto.Email);
        await _authBusinessRules.UserShouldBeExistsWhenSelected(user);
        await _authBusinessRules.UserPasswordShouldBeMatch(user!.Id, userForLoginDto.Password);


        LoggedResponse loggedResponse = new();

        //if (user.AuthenticatorType is not AuthenticatorType.None)
        //{
        //    if (request.UserForLoginDto.AuthenticatorCode is null)
        //    {
        //        await _authenticatorService.SendAuthenticatorCode(user);
        //        loggedResponse.RequiredAuthenticatorType = user.AuthenticatorType;
        //        return loggedResponse;
        //    }

        //    await _authenticatorService.VerifyAuthenticatorCode(user, request.UserForLoginDto.AuthenticatorCode);
        //}

        Core.Security.JWT.AccessToken createdAccessToken = await CreateAccessToken(user);

        Core.Security.Entities.RefreshToken createdRefreshToken = await CreateRefreshToken(user, ipAddress);
        Core.Security.Entities.RefreshToken addedRefreshToken = await AddRefreshToken(createdRefreshToken);
        await DeleteOldRefreshTokens(user.Id);

        loggedResponse.AccessToken = createdAccessToken;
        loggedResponse.RefreshToken = addedRefreshToken;
        return loggedResponse;
    }

    public async Task<RegisteredResponse> Register(UserForRegisterDto userForRegisterDto, string ipAddress)
    {
        //await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

        HashingHelper.CreatePasswordHash(
            userForRegisterDto.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        User newUser =
            new()
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

        CreateUserRequest createUserRequest = _mapper.Map<CreateUserRequest>(newUser);
        CreatedUserResponse createdUserResponse = await _userService.AddAsync(createUserRequest);
        User user = _mapper.Map<User>(createdUserResponse);

        Core.Security.JWT.AccessToken createdAccessToken = await CreateAccessToken(user);

        Core.Security.Entities.RefreshToken createdRefreshToken = await CreateRefreshToken(user, ipAddress);
        Core.Security.Entities.RefreshToken addedRefreshToken = await AddRefreshToken(createdRefreshToken);

        RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
        return registeredResponse;
    }
}
