using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.DTOs
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwrExpireDays { get; set; }
        public string JwtIssuer { get; set; }

    }
}
