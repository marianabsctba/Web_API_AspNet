using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Infra.Data.Data;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Ioc
{
    public static class DependencyInjection
    {

        public static void RegisterServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<WebDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WebDbContext")));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDonationService, DonationService>();
            services.AddTransient<IDonationRepository, DonationRepository>();
        }
    }
}
