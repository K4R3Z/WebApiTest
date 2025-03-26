using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiTest.Auth;
using WebApiTest.Context;
using WebApiTest.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DeviceContext>();
builder.Services.AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API Test", Version = "v1" });
    c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
