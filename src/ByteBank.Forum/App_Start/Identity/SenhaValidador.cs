using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ByteBank.Forum.App_Start.Identity
{
    public class SenhaValidador : IIdentityValidator<string>
    {
        public int TamanhoRequerido { get; set; }
        public bool CaracterEspecial { get; set; }
        public bool LowerCase { get; set; }
        public bool UperCase { get; set; }
        public bool CaracterDigito { get; set; }

        public async Task<IdentityResult> ValidateAsync(string senha)
        {
            var erros = new List<string>();

            if (!VerificaTamanho(senha))
            {
                erros.Add($"A senha deve conter caracteres no mínimo {TamanhoRequerido} caracteres");
            }

            if (CaracterEspecial && !VerificaCaracterEspecial(senha))
            {
                erros.Add("A senha deve conter caracteres especiais");
            }

            if (LowerCase && !VerificaLowerCase(senha))
            {
                erros.Add("A senha deve conter caracteres minúsculos");
            }

            if (UperCase && !VerificaUperCase(senha))
            {
                erros.Add("A senha deve conter caracteres maiúsculos");
            }

            if (CaracterDigito && !VerificaDigito(senha))
            {
                erros.Add("A senha deve conter caracteres digitos");
            }

            if (erros.Any())
            {
                return IdentityResult.Failed(erros.ToArray());
            }
            else
            {
                return IdentityResult.Success;
            }
        }

        private bool VerificaTamanho(string senha) => senha?.Length >= TamanhoRequerido;

        private bool VerificaCaracterEspecial(string senha) => Regex.IsMatch(senha, @"[!#$%&'()*+,-./:;?@[\\\]_`{|}~]");

        private bool VerificaLowerCase(string senha) => senha.Any(char.IsLower);

        private bool VerificaUperCase(string senha) => senha.Any(char.IsUpper);

        private bool VerificaDigito(string senha) => senha.Any(char.IsDigit);
    }
}