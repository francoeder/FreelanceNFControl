using FreelanceNFControl.Domain.Configuration;
using FreelanceNFControl.Domain.DbContext;
using FreelanceNFControl.Domain.Interfaces;
using FreelanceNFControl.Domain.Services;
using FreelanceNFControl.Infra.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreelanceNFControl.Domain.DependencyInjetion
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<Migrator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();

            services.AddAutoMapper(typeof(AutoMapperConfiguration));

            return services;
        }
    }
}
