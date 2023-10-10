using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.DTOs.Responses
{
    public class LoginResponse
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
