
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Mvc.Models;
using Cooperchip.ITDeveloper.Mvc.ViewModels;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    [Route("")]
    [Route("gestao-de-paciente")]
    [Route("gestao-de-pacientes")]
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger _looger;
        private readonly IUserInContext _user;
        private readonly IUserInAllLayer _userInAllLayer;

        public HomeController(IEmailSender emailSender, ILogger looger, IUserInAllLayer userInAllLayer, IUserInContext user)
        {
            _emailSender = emailSender;
            _looger = looger;
            _userInAllLayer = userInAllLayer;
            _user = user;
        }

        [Route("")]
        [Route("pagina-inicial")]
        public IActionResult Index()
        {          
            return View();
        }

        //[Authorize(Roles = "Admin")]
        [Route("dashboard")]
        [Route("pagina-de-estatistica")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("box-init")]
        public IActionResult BoxInit()
        {
            return View();
        }

        [Route("quem-somos")]
        [Route("sobre-nos")]
        [Route("sobre/{id:guid}/{paciente}/{categoria?}")]
        public IActionResult Sobre(Guid id, string paciente, string categoria)
        {
            return View();
        }

        [HttpGet(template:"fale-conosco")]
        //[Route("Fale-conosco")]
        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        [Route("Fale-conosco")]
        public async Task<IActionResult> Contato(ContatoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailSender.SendEmailAsync(model.Email, model.Subject, model.Message);
                    _looger.Log(LogLevel.Information, message: "Email enviado com sucesso!");
                    return RedirectToAction("Index", controllerName: "Home");
                }
                catch (Exception e )
                {
                    _looger.Log(LogLevel.Error, message: $"Erro tentando enviar email: {e.Message}");
                    throw;
                }
            }
            return View();
        }

        [Route("privacidade")]
        [Route("politica-de-privacidade")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro")]
        [Route("erro-encontrado")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
