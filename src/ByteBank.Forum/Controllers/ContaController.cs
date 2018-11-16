using System.Web.Mvc;
using ByteBank.Forum.Models;
using ByteBank.Forum.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ByteBank.Forum.Controllers
{
    public class ContaController : Controller
    {
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(ContaRegistrarViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                var dbContext = new IdentityDbContext<UsuarioAplicacao>("DefaultConnection");
                var userStore = new UserStore<UsuarioAplicacao>(dbContext);
                var userManager = new UserManager<UsuarioAplicacao>(userStore);
                var novoUsuario = new UsuarioAplicacao();

                novoUsuario.Email = modelo.Email;
                novoUsuario.UserName = modelo.UserName;
                novoUsuario.NomeCompleto = modelo.NomeCompleto;

                userManager.Create(novoUsuario, modelo.Senha);

                return RedirectToAction("Index", "Home");
            }

            return View(modelo);
        }
    }
}