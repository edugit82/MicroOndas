using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace MicroOndas.Interface.Business
{
    public class CadastrarBusiness 
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration, CadastrarViewModel cadastrar) 
        {
            RetornoAquecimentoViewModel retorno = _retorno;
            
            cadastrar.Login = cadastrar.Login?.ToUpper().Trim();
            cadastrar.Senha = cadastrar.Senha?.Trim();
            cadastrar.ReSenha = cadastrar.ReSenha?.Trim();

            if (cadastrar.Login?.Trim() == string.Empty)
            {
                retorno.Mensagem = "Login não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";

                return;
            }

            if (cadastrar.Senha?.Trim() == string.Empty)
            {
                retorno.Mensagem = "Senha não pode ser vazia!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (cadastrar.ReSenha?.Trim() == string.Empty)
            {
                retorno.Mensagem = "Repetir Senha não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (cadastrar.Senha != cadastrar.ReSenha)
            {
                retorno.Mensagem = "Senhas não conferem!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            LogDeErros.Log(ref retorno, () =>
            {
                string hash = ConverterStringSHA1.Converter(cadastrar.Senha ?? "");
                
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {
                    cadastrar.Login = cadastrar.Login?.ToUpper().Trim();
                    bool exist = ctx.Login.ToList().Exists(x => x.Usuario == cadastrar.Login);

                    if (exist)
                    {
                        retorno.Mensagem = "Usuário já cadastrado!";
                        retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                        return;
                    }

                    LoginModel model = new LoginModel
                    {
                        Usuario = cadastrar.Login,
                        Senha = hash
                    };

                    ctx.Login.Add(model);
                    ctx.SaveChanges();

                    retorno.Mensagem = "Usuário cadastrado com sucesso!";
                    retorno.Mensagem = "<span style='color:green;'>" + retorno.Mensagem + "</span>";
                }
            });

            _retorno = retorno;
        }
    }
}
