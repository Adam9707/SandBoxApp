namespace SandBoxApp.API.Middlewares
{
    public static class MiddlewaresInstaller
    {
        public static IServiceCollection AddSandBoxAppMiddlewares(this IServiceCollection services) 
        { 
            services.AddScoped<ErrorHandlingMiddleware> ();
            return services;
        }
    }
}
