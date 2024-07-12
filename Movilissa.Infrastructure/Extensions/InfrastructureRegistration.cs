
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Data.Repositories;
using Movilissa_api.Models;
using Movilissa.core.Interfaces;
using Movilissa.Infrastructure.Repositories;

namespace Movilissa_api.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Configuring DI for Core and Infrastructure layersa
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISearchRepository, SearchRepository>();
            // Añade más repositorios según sea necesario
            
            // Configuración de DI para capas Core e Infrastructure
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISearchRepository, SearchRepository>();

            return services;
        }
    }
}
