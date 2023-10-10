using SandBoxApp.Application.DTOs;
using SandBoxApp.Application.DTOs.Requests;
using SandBoxApp.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.Contracts
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginRequest loginDTO);
        void Register(RegisterUserDTO registerUserDTO);
    }
}
