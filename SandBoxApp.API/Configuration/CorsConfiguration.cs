using System.Runtime.CompilerServices;

namespace SandBoxApp.API.Configuration
{
    public static class CorsConfiguration
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy("developmentCorsPolicy", builder =>
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

            return services;
        }
    }
}
