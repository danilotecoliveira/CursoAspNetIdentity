using Owin;
using Microsoft.Owin;
using System.Data.Entity;
using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using ByteBank.Forum.App_Start.Identity;

[assembly: OwinStartup(typeof(ByteBank.Forum.Startup))]
namespace ByteBank.Forum
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.CreatePerOwinContext<DbContext>(() => new IdentityDbContext<UsuarioAplicacao>("DefaultConnection"));
            builder.CreatePerOwinContext<IUserStore<UsuarioAplicacao>>((opcoes, contextoOwin) => {
                var dbContext = contextoOwin.Get<DbContext>();
                return new UserStore<UsuarioAplicacao>(dbContext);
            });

            builder.CreatePerOwinContext<UserManager<UsuarioAplicacao>>((opcoes, contextoOwin) => {
                var userStore = contextoOwin.Get<IUserStore<UsuarioAplicacao>>();
                var userManager = new UserManager<UsuarioAplicacao>(userStore);

                var userValidator = new UserValidator<UsuarioAplicacao>(userManager);
                userValidator.RequireUniqueEmail = true;
                userManager.UserValidator = userValidator;

                userManager.PasswordValidator = new SenhaValidador
                {
                    TamanhoRequerido = 6,
                    CaracterEspecial = true,
                    LowerCase = true,
                    UperCase = true,
                    CaracterDigito = true
                };

                return userManager;
            });
        }
    }
}