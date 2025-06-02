using MicroOndas.DataBase;
using MicroOndas.Public;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class LogarBusiness : _Business
    {
        public LogarBusiness(LogarViewModel viewmodel, bool logado) 
        {
            //Está logado
            if (!logado)
            {
                this.Retorno = "Token inválido!";
                this.Cor = "red";

                return;
            }

            bool gravaerro = true;
            try
            {
                viewmodel.Login = viewmodel.Login.ToUpper().Trim();
                viewmodel.Senha = viewmodel.Senha.Trim();

                if (viewmodel.Login.Length < 5)
                    throw new LogarLoginInvalidoException("O login deve ter no mínimo 5 caracteres.");

                if (viewmodel.Senha.Trim() == string.Empty)
                    throw new LogarSenhaInvalidoException("Senha não pode ser vazia.");

                string hash = ConverterStringSHA1.Converter(viewmodel.Senha.Trim() ?? "");

                using (Context ctx = new Context())
                {
                    bool exist = ctx.Login.ToList().Exists(a => a.Usuario == viewmodel.Login && a.Senha == hash);

                    if (!exist)
                        throw new LogarUsuarioInvalidoException("Usuário ou senha inválidos!");

                    this.Retorno = "Usuário logado com sucesso!";
                    this.Cor = "green";
                }
            }
            catch (LogarLoginInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (LogarSenhaInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (LogarUsuarioInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (Exception ex)
            {
                if (gravaerro)
                    Excecao.GravaExcecao(ex);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }        
    }
}
