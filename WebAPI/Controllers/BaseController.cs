using Core.Security.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BaseController : ControllerBase
{
    protected string getIpAddress()
    {
        string ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
                ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");
        return ipAddress;
    }

    protected int getUserIdFromRequest() 
    {
        int userId = HttpContext.User.GetUserId();
        return userId;
    }
}
