using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateAccessToken(User user , IList<OperationClaim> operationClaims);
    RefreshToken CreateRefreshToken(User user, string ipAddress);

}
