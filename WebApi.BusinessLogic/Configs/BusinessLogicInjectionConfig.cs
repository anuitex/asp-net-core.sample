using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.BusinessLogic.Helpers;
using WebApi.BusinessLogic.Helpers.Interfaces;
using WebApi.BusinessLogic.Hubs;
using WebApi.BusinessLogic.Services;
using WebApi.BusinessLogic.Services.Interfaces;
using WebApi.DataAccess.Configs;

namespace WebApi.BusinessLogic.Configs
{
    public static class BusinessLogicInjectionConfig
    {
        public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.InjectDataAccessDependency(configuration);
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<NotificationHub>();
            services.AddScoped<IUserFilesHelper, UserFilesHelper>();
        }
    }
}
