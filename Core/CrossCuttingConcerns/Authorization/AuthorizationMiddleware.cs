using Azure.Core;
using Core.Attributes.Authorization;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Security.Constants;
using Core.Security.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Authorization;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
       
        if (endpoint?.Metadata?.GetMetadata<AuthorizationAttribute>() is { Roles: { } roles })
        {
            List<string>? userRoleClaims = context.User.ClaimRoles();
           

            if (userRoleClaims.IsNullOrEmpty())
                ThrowForbidden("You are not authenticated.");

            bool isNotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .FirstOrDefault(userRoleClaim =>
            userRoleClaim == GeneralOperationClaims.Admin || roles.Any(role => role == userRoleClaim))
            .IsNullOrEmpty();

            if (isNotMatchedAUserRoleClaimWithRequestRoles)
               ThrowForbidden("You are not authorized.");

        }


        await _next(context);
     

    }

    private static void ThrowForbidden(string message)
    {
        var exception = new Exception(message);
        exception.Data["HttpStatusCode"] = StatusCodes.Status403Forbidden;
        throw exception;
    }
}
