using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.API.Filters;
using WebApi.API.Middleware;
using WebApi.API.Models;
using WebApi.BusinessLogic.Configs;
using WebApi.BusinessLogic.Hubs;
using WebApi.Config;
using static WebApi.Config.Constants.Strings;

namespace WebApi.API
{
    public class Startup
    {
        private const string _corsPolicyName = "MyPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InjectBusinessLogicDependency(Configuration);

            services.InjectConfigLogicDependency(Configuration);

            ConfigureRequestsAccountInjection(services);

            services.AddCors(o => o.AddPolicy(_corsPolicyName, builder =>
            {
                builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelStateValidationActionFilter));
            });

            ConfigureSwagger(services);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ASP.NET Core Identity Web API",
                    Description = "A simple example ASP.NET Core Identity Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Artur Bardachov",
                        Email = "artydev0101@gmail.com",
                        Url = "https://google.com/"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://google.com/license"
                    }
                });

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                options.AddSecurityRequirement(security);
            });
        }

        private static void ConfigureRequestsAccountInjection(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient((serviceProvider) =>
            {
                IHttpContextAccessor httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

                ClaimsPrincipal user = httpContextAccessor.HttpContext.User;

                string accountIdValue = user.FindFirstValue(JwtClaimIdentifiers.Id);
                string email = user.FindFirstValue(ClaimTypes.NameIdentifier);

                long.TryParse(accountIdValue, out long accountId);

                var accountLite = new AccountLite
                {
                    Id = accountId,
                    Email = email
                };

                return accountLite;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseCors(_corsPolicyName);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notification");
                routes.MapHub<FileTransferHub>("/file-transfer");
            });

            app.UseMvc();

            app.UseHttpsRedirection();
        }
    }
}
