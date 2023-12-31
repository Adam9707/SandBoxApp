﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.DTOs
{
    public class RegisterUserDTO
    {
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
