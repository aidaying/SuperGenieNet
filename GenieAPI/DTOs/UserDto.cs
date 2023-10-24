using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GenieAPI.DTOs;

public class UserDto
{
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public string Token { get; set; }
}
