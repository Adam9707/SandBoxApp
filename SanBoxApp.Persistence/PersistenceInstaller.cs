using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SanBoxApp.Persistence;
using SandBoxApp.Application.Contracts;
using SandBoxApp.Domain.Entities;
using SandBoxApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Persistence
{
    public static class PersistenceInstaller
    {
        public static IServiceCollection AddSandBoxAppPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<SandBoxAppDbContext>(options =>
                options.UseSqlServer(connectionString));
             services.AddScoped<IUnitOfWork, UnitOfWork>();
          //  services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

            return services;
        }
    }
}
