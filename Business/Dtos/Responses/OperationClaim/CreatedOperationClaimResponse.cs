using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.OperationClaim;

public class CreatedOperationClaimResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CreatedOperationClaimResponse()
    {
        Name = string.Empty;
    }

    public CreatedOperationClaimResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
