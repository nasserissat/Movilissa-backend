using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Data.Repositories;
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


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

builder.Services.AddScoped(typeof(GenericLogic<>));
builder.Services.AddScoped(typeof(SearchLogic));


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowWebApp");

app.Run();