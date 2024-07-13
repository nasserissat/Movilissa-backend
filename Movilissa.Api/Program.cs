using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Data.Repositories;
using Movilissa_api.Infrastructure.Extensions;
using Movilissa_api.Logic;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});



// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowWebApp");

app.Run();