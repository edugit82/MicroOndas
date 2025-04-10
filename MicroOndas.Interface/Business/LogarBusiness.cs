using MicroOndas.Business;
using MicroOndas.DataBase;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MicroOndas.Interface.Business
{
    public class LogarBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration, LogarViewModel logar)
        {
            RetornoAquecimentoViewModel retorno = _retorno;

            logar.Login = logar.Login?.ToUpper().Trim();
            logar.Senha = logar.Senha?.Trim();

            if (logar.Login?.Trim() == string.Empty)
            {
                retorno.Mensagem = "Login não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (logar.Senha?.Trim() == string.Empty)
            {
                retorno.Mensagem = "Senha não pode ser vazia!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            LogDeErros.Log(ref retorno, () =>
            {
                string hash = ConverterStringSHA1.Converter(logar.Senha ?? "");
                
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {
                    bool exist = ctx.Login.ToList().Exists(a => a.Usuario == logar.Login && a.Senha == hash);

                    if (!exist)
                    {
                        retorno.Mensagem = "Usuário ou senha inválidos!";
                        retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                        return;
                    }
                    else
                    {
                        retorno.Mensagem = "Usuário logado com sucesso!";
                        retorno.Mensagem = "<span style='color:green;'>" + retorno.Mensagem + "</span>";
                    }

                    retorno.Token = GenerateJwtToken(logar.Login ?? "",_configuration);
                }
            });

            _retorno = retorno;
        }
        private string GenerateJwtToken(string usuario, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim("Admin", "Eduardo"),
              new Claim("User", usuario)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,

                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
