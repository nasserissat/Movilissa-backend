using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Infrastructure.Extensions;
using Movilissa_api.Logic;
using Movilissa_api.Models;
using Movilissa.core;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
// Add services to the container.



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>(
        options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<CustomIdentityErrorDescriber>();


builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://localhost:5054",
        ValidIssuer = "http://localhost:5054",
        RequireExpirationTime = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the key that we will use in the encryption")),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var app = builder.Build();
if(app.Environment.IsDevelopment() || app.Environment.IsProduction()){
    app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseCors(b => b
    .AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()); 
app.UseAuthentication();;
app.UseAuthorization();;
app.MapControllers();
app.MapControllers().RequireAuthorization();

app.Run();