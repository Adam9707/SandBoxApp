using SandBoxApp.API.Configuration;
using SandBoxApp.API.Middlewares;
using SandBoxApp.Application;
using SandBoxApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSandBoxAppPersistence(builder.Configuration);
builder.Services.AddSandBoxAppApplication();
builder.Services.AddSandBoxAppAuthentication(builder.Configuration);
builder.Services.AddSandBoxAppMiddlewares();
builder.Services.ConfigureCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("developmentCorsPolicy");
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
