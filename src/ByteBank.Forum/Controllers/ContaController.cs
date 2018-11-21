using System.Web;
using System.Web.Mvc;
using ByteBank.Forum.Models;
using System.Threading.Tasks;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        private UserManager<UsuarioAplicacao> _userManager;
        public UserManager<UsuarioAplicacao> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var contextoOwin = HttpContext.GetOwinContext();
                    _userManager = contextoOwin.GetUserManager<UserManager<UsuarioAplicacao>>();
                }

                return _userManager;
            }
            set
            {
                _userManager = value;
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var novoUsuario = new UsuarioAplicacao();

                novoUsuario.Email = modelo.Email;
                novoUsuario.UserName = modelo.UserName;
                novoUsuario.NomeCompleto = modelo.NomeCompleto;

                var usuario = await UserManager.FindByEmailAsync(novoUsuario.Email);
                var usuarioExiste = usuario != null;

                if (usuarioExiste)
                {
                    return View("AguardandoConfirmacao");
                }

                var resultado = await UserManager.CreateAsync(novoUsuario, modelo.Senha);

                if (resultado.Succeeded)
                {
                    await EnviarEmailConfirmacao(novoUsuario);

                    return View("AguardandoConfirmacao");
                }
                else
                {
                    AdicionaErros(resultado);
                }
            }

            return View(modelo);
        }

        public ActionResult ConfirmacaoEmail(string usuarioId, string token)
        {

        }

        private async Task EnviarEmailConfirmacao(UsuarioAplicacao usuario)
        {
            var token = await UserManager.GenerateEmailConfirmationTokenAsync(usuario.Id);
            var link = Url.Action("ConfirmacaoEmail", "Conta", new { usuarioId = usuario.Id, token = token }, Request.Url.Scheme);

            await UserManager.SendEmailAsync(usuario.Id, "Confirmação de e-mail", $"Bem vindo ao Forum, clique aqui {link} para confirmar seu e-mail.");
        }

        private void AdicionaErros(IdentityResult resultado)
        {
            foreach (var item in resultado.Errors)
            {
                ModelState.AddModelError("", item);
            }
        }
    }
}