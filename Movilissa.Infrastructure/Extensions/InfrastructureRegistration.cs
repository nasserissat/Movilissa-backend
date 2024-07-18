
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movilissa_api.Data.Context;
using Movilissa_api.Data.IRepositories;
using Movilissa_api.Logic;
using Movilissa.core.Interfaces;
using Movilissa.core.Interfaces.IServices;
using Movilissa.Infrastructure.Repositories;

namespace Movilissa_api.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b =>
                b.MigrationsAssembly("Movilissa.Infrastructure")));
            
            // Configuring DI for Repositories
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IBusScheduleRepository, BusScheduleRepository>();
            
            // Configuring DI for Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<ICompanyService, CompanyService>();

            

            return services;
        }
    }
}
