using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using MicroOndas.Public;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class CadastrarBusiness : _Business
    {        
        public CadastrarBusiness(CadastrarViewModel viewmodel, bool logado) 
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
                viewmodel.Confirma = viewmodel.Confirma.Trim();

                if (viewmodel.Login.Length < 5)
                    throw new CadastrarLoginInvalidoException("O login deve ter no mínimo 5 caracteres.");

                if (viewmodel.Senha.Trim() == string.Empty)
                    throw new CadastrarSenhaInvalidoException("Senha não pode ser vazia.");

                if (viewmodel.Confirma.Trim() == string.Empty)
                    throw new CadastrarRepetirSenhaInvalidoException("Repetir senha não pode ser vazia.");

                if (viewmodel.Senha != viewmodel.Confirma)
                    throw new CadastrarRepetirDiferenteSenhaInvalidoException("Repetir senha diferente de senha.");

                string hash = ConverterStringSHA1.Converter(viewmodel.Senha.Trim() ?? "");

                using (Context ctx = new Context()) 
                {
                    bool exist = ctx.Login.ToList().Exists(x => x.Usuario == viewmodel.Login);

                    if (exist)
                        throw new CadastrarUsuarioJaCadastradoException("Usuario Já Cadastrado!");

                    LoginModel model = new LoginModel
                    {
                        Usuario = viewmodel.Login,
                        Senha = hash
                    };

                    ctx.Login.Add(model);
                    ctx.SaveChanges();
                    
                    this.Retorno = "Usuário cadastrado com sucesso!";
                    this.Cor = "green";
                }
            }
            catch (CadastrarLoginInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarSenhaInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarRepetirSenhaInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarRepetirDiferenteSenhaInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarUsuarioJaCadastradoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (Exception ex)
            {
                if(gravaerro)
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
