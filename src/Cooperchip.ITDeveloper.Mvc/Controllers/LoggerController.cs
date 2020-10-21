using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            var usuario = HttpContext.User.Identity.Name;

            _logger.Trace(message: $"O Usuário: {usuario} foi que fez isso!");


            try
            {
                throw new Exception(message: "ANTEÇÃO. \n Um erro proposital Ocorreu! \nCONTATE O ADMINISTRADOR DO SISTEMA!");
            }
            catch (Exception e)
            {

                _logger.Error(message: $"{e} -Usuario Logado: {usuario}");
            }
            //throw new Exception(message: "ANTEÇÃO. \n Um erro proposital Ocorreu! \nCONTATE O ADMINISTRADOR DO SISTEMA!");

            return View();
        }
    }
}