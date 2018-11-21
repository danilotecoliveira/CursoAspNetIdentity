using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ByteBank.Forum.App_Start.Identity
{
    public class EmailServico : IIdentityMessageService
    {
        private readonly string _emailOrigem = ConfigurationManager.AppSettings["emailServico:email_rementente"];
        private readonly string _senha = ConfigurationManager.AppSettings["emailServico:email_senha"];

        public async Task SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}