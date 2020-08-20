using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Extensions;
using KissLog;
using ILogger = KissLog.ILogger;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger _logger;  

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Trace("Usuário acessou a home!");
            _logger.Debug("Usuário acessou a home 2 !");
            return View();
        }

     
        public IActionResult Privacy()
        {
            throw new Exception("Erro");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult secret()
        {
                return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View("secret");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimGravar()
        {
            return View("secret");
        }

        [ClaimsAuthorize("Produtos","Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("secret");
        }

       [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Accesso Negado";
                modelErro.ErrorCode = id;
            }
            else return StatusCode(404);

            return View("Error", modelErro);
        }
    }
}
