using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes.Authorization;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public class AuthorizationAttribute : Attribute
{

    public string[] Roles { get; set; }

    public AuthorizationAttribute(string[] roles)
    {
        Roles = roles;
    }
}
