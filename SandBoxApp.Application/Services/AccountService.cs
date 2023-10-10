using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SandBoxApp.Application.Contracts;
using SandBoxApp.Application.DTOs;
using SandBoxApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SandBoxApp.Application.DTOs.Requests;
using SandBoxApp.Application.DTOs.Responses;

namespace SandBoxApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public async Task<LoginResponse> Login(LoginRequest loginDTO)
        {

            var user = await _unitOfWork.Repository<User>().GetOneAsync(u => u.Email == loginDTO.Login || u.NickName == loginDTO.Login, u => u.Role);

            if (user is null)
            {
                throw new AuthenticationException("Invalid username or password");
            }
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password) == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Invalid username or password");
            }

            var token = GenerateJwtAsync(user);

            return new LoginResponse {
                NickName = user.NickName,
                Email = user.Email,
                Token = token 
            };
        }
      
        public void Register(RegisterUserDTO registerUserDTO)
        {
            var newUser = new User
            {
                Name = registerUserDTO.Name,
                Surname = registerUserDTO.Surname,
                Email = registerUserDTO.Email,
                NickName = registerUserDTO.NickName,
                DateOfBirth = registerUserDTO.DateOfBirth,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerUserDTO.Password);
            using ((IDisposable)this._unitOfWork)
            {             
                _unitOfWork.Repository<User>().Insert(newUser);
            }
        }
        private string GenerateJwtAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwrExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
    }
}
