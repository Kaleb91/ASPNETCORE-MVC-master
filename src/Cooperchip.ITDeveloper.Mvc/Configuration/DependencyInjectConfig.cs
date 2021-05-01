using Cooperchip.ITDeveloper.Application.Servicos;
using Cooperchip.ITDeveloper.CrossCutting.Auxiliar;
using Cooperchip.ITDeveloper.CrossCutting.Helpers;
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Domain.Interfaces.Entidades;
using Cooperchip.ITDeveloper.Mvc.Extensions.Filters;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Infra;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IRepositoryDomainPaciente, PacienteService>();

            services.AddTransient<IUnitOfUpload, UnitOfUpload>();
            

            //=========/Mantem o estado do contexto Http por toda  a aplicação ==//
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //===================================================================//
            services.AddScoped<IUserInContext, AspNetUsers>();
            services.AddScoped<IUserInAllLayer, UserInAllLayer>();
            //===================================================================//

            //=====Adicionar Claims para HttpContext Toda Aplications===========//
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsService>();
            //===================================================================//

            services.AddScoped((context) => Logger.Factory.Get());

            services.AddScoped<AuditoriaILoggerFilter>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(configuration);

            return services;
        }
    }
}
