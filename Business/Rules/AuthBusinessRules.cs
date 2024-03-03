using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules;

public class AuthBusinessRules
{

    private readonly IUserDal _userDal;
   

    public AuthBusinessRules(IUserDal userDal)
    {
        _userDal = userDal;
      
    }


    //public async Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    //{
    //    if (emailAuthenticator is null)
    //        throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
    //}

    //public async Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is null)
    //        throw new BusinessException(AuthMessages.OtpAuthenticatorDontExists);
    //}

    //public async Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
    //        throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    //}

    //public async Task EmailAuthenticatorActivationKeyShouldBeExists(EmailAuthenticator emailAuthenticator)
    //{
    //    if (emailAuthenticator.ActivationKey is null)
    //        throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
    //}

    public async Task UserShouldBeExistsWhenSelected(User? user)
    {
        if (user == null)
            throw new BusinessException("User Dont Exists");
    }

    //public async Task UserShouldNotBeHaveAuthenticator(User user)
    //{
    //    if (user.AuthenticatorType != AuthenticatorType.None)
    //        throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
    //}

    public async Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException("Refresh Dont Exists");
    }

    public async Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
            throw new BusinessException("Invalid Refresh Token");
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        var doesExists = await _userDal.GetAsync(predicate: u => u.Email == email);
        if (doesExists!=null)
            throw new BusinessException("User Mail Already Exists");
    }

    public async Task UserPasswordShouldBeMatch(int id, string password)
    {
        User? user = await _userDal.GetAsync(predicate: u => u.Id == id);
        await UserShouldBeExistsWhenSelected(user);
        if (!HashingHelper.VerifyPasswordHash(password, user!.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Password Dont Match");
    }

}
