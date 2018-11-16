using Microsoft.AspNet.Identity.EntityFramework;

namespace ByteBank.Forum.Models
{
    public class UsuarioAplicacao : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}