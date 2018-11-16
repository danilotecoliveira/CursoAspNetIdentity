using Owin;
using Microsoft.Owin;
using System.Data.Entity;
using ByteBank.Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

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
                return new UserManager<UsuarioAplicacao>(userStore);
            });
        }
    }
}