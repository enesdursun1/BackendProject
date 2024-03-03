using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos;

public class UserForLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    

    public UserForLoginDto()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    public UserForLoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
