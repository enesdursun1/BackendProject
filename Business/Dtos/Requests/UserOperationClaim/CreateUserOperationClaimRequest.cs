﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests.UserOperationClaim;

public class CreateUserOperationClaimRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }


}
