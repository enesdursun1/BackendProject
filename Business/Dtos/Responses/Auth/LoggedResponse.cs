using Azure;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.Auth;

public class LoggedResponse
{
    public AccessToken? AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }
  

    public LoggedHttpResponse ToHttpResponse() =>
        new() { AccessToken = AccessToken };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
       
    }
}
