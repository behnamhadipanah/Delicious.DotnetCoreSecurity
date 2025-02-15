using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Scalar.AspNetCore;
using SecurityApp.Api.Algorithms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IAuthenticatedEncryptorFactory, CustomAuthenticatedEncryptorFactory>();

builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference ();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
