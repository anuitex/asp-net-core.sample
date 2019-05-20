using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DataAccess.Entities;
using WebApi.DataAccess.Repositories.Dapper;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.DataAccess.Configs
{
    public static class DataAccessInjectionConfig
    {
        public static void InjectDataAccessDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookGenreRepository, BookGenreRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            string dbConnectionString = configuration.GetConnectionString("DefaultConnection");
            string assemblyName = typeof(ApplicationContext).Namespace;

            services.AddDbContext<ApplicationContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(dbConnectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(assemblyName))
            );

            services.AddIdentity<Account, AccountRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddSingleton<DBInitializeConfig>();

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                DBInitializeConfig dbInitializeConfig = serviceProvider.GetService<DBInitializeConfig>();
                dbInitializeConfig.InitializeDatabase().GetAwaiter().GetResult();
            }
        }
    }
}
