using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Filters
{
    public class AuditoriaILoggerFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public AuditoriaILoggerFilter(ILogger logger)
        {
            _logger = logger;  
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Todo: Logar algo antes da Execução.
            _logger.Info(message: $"Url Acessada: {context.HttpContext.Request.GetDisplayUrl()} \n\n_________________________________\n\n");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = context.HttpContext.User.Identity.Name;
                var tipoAuth = context.HttpContext.User.Identity.AuthenticationType;
                var urlAcessada = context.HttpContext.Request.GetDisplayUrl();
                var valueHost = context.HttpContext.Request.Host.Value;
                var typeContent = context.HttpContext.Request.ContentType;

                var logMsg = $"O usuário {user} acessou a Url {urlAcessada} \nEm: {DateTime.Now} \n ============================" +
                    $"\n{valueHost} \n {typeContent} \n Tipo de Autenticação: {tipoAuth}";


                _logger.Info(logMsg);
            }
        }

       
    }
}
