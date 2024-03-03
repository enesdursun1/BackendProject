using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.OperationClaim;

public class CreateOperationClaimRequest
{
    public string Name { get; set; }

    public CreateOperationClaimRequest()
    {
        Name = string.Empty;
    }

    public CreateOperationClaimRequest(string name)
    {
        Name = name;
    }


}
