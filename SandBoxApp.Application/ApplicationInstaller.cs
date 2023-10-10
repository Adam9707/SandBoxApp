using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SandBoxApp.Application.Contracts;
using SandBoxApp.Application.Services;
using SandBoxApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddSandBoxAppApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
