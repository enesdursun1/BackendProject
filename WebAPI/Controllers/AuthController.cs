using Business.Abstract;
using Business.Dtos.Responses.Auth;
using Core.Entities.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly WebApiConfiguration _configuration;
    private IAuthService _authService;

    public AuthController(IConfiguration configuration, IAuthService authService)
    {
        _authService = authService;
        const string configurationSection = "WebAPIConfiguration";
        _configuration =
            configuration.GetSection(configurationSection).Get<WebApiConfiguration>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {

          LoggedResponse result = await _authService.Login(userForLoginDto, getIpAddress());
            if (result.RefreshToken is not null)
                setRefreshTokenToCookie(result.RefreshToken);

            return Ok(result.ToHttpResponse());
     

     
      
       
       

    
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        RegisteredResponse result = await _authService.Register(userForRegisterDto, getIpAddress());
       
     
        setRefreshTokenToCookie(result.RefreshToken);
        return Created(uri: "", result.AccessToken);
    }

    //[HttpGet("RefreshToken")]
    //public async Task<IActionResult> RefreshToken()
    //{
    //    RefreshTokenCommand refreshTokenCommand = new() { RefreshToken = getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
    //    RefreshedTokensResponse result = await Mediator.Send(refreshTokenCommand);
    //    setRefreshTokenToCookie(result.RefreshToken);
    //    return Created(uri: "", result.AccessToken);
    //}

    //[HttpPut("RevokeToken")]
    //public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
    //{
    //    RevokeTokenCommand revokeTokenCommand = new() { Token = refreshToken ?? getRefreshTokenFromCookies(), IpAddress = getIpAddress() };
    //    RevokedTokenResponse result = await Mediator.Send(revokeTokenCommand);
    //    return Ok(result);
    //}



    private string getRefreshTokenFromCookies() =>
        Request.Cookies["refreshToken"] ?? throw new ArgumentException("Refresh token is not found in request cookies.");

    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key: "refreshToken", refreshToken.Token, cookieOptions);
    }

}
