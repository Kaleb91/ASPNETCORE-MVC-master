using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Mvc.Configuration;
using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.Filters;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Identity.Services;
using KissLog;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

namespace Cooperchip.ITDeveloper.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", true, true)
                .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            if (env.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextConfig(Configuration); // Nossa DbContext (Banco de Dados)
            services.AddIdentityConfig(Configuration); // Está em AddIdentityConfig 
            services.AddMvcAndRazor(); //Está em MvcAndRazor
            services.AddDependencyInjectConfig(Configuration); // Está em DependencyInjectConfig          

            //Fornece acesso a um provedorde codificação para páginas de código
            //Quede outra forma estão disponivel apenas no .Net Framework para Desktop

            services.AddProviderPageCode();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context , UserManager<ApplicationUser> usermanager,
                              RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/logger/Index");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsProduction())
            {
                app.UseKissLogMiddleware(options =>
                {
                    options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                        Configuration["KissLog.OrganizationId"],
                        Configuration["KissLog.ApplicationId"])
                    ));
                });
            }

            var authMsgSenderOpt = new AuthMessageSenderOptions
            {
                SendGridUser = Configuration["SendeGridUser"],
                SendGridKey = Configuration["SendGridKey"]
            };

            DefaultUsersAndRoles.Seed(context, usermanager, roleManager).Wait();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoint.MapRazorPages();
            }
            );           
        }
    }
}
