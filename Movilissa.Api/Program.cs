using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Infrastructure.Extensions;
using Movilissa_api.Logic;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateSlimBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.Run();