using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using Microsoft.Extensions.Configuration;
using System;

namespace MicroOndas.Interface.Business
{
    public class CadastrarProgramaBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration, CadastroProgramaViewModel programa) 
        {
            RetornoAquecimentoViewModel retorno = _retorno;
            retorno.PararProgresso = false;

            programa.Nome = programa.Nome?.Trim();
            programa.Alimento = programa.Alimento?.Trim();
            programa.Tempo = programa.Tempo?.Trim();
            programa.Progresso = programa.Progresso?.Trim();
            programa.Instrucoes = programa.Instrucoes?.Trim();

            if (programa.Nome == string.Empty)
            {
                retorno.Mensagem = "Nome não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (programa.Alimento == string.Empty)
            {
                retorno.Mensagem = "Alimento não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (programa.Tempo == string.Empty)
            {
                retorno.Mensagem = "Tempo não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (programa.Tempo?.Length < 5)
            {
                retorno.Mensagem = "Tempo precisa ter o formato 00:00!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (programa.Potencia < 1)
            {
                retorno.Mensagem = "Potência não pode ser menor que 1!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            if (programa.Progresso == string.Empty)
            {
                retorno.Mensagem = "Progresso não pode ser vazio!";
                retorno.Mensagem = "<span style='color:red;'>" + retorno.Mensagem + "</span>";
                return;
            }

            LogDeErros.Log(ref retorno, () =>
            {
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {
                    ProgramadoModel model = new ProgramadoModel
                    {
                        Nome = programa.Nome,
                        Alimento = programa.Alimento,
                        Tempo = DateTime.Parse(string.Format("01-01-1970 00:{0}", programa.Tempo)),
                        Potencia = programa.Potencia,
                        Caracter = programa.Progresso,
                        Intrucoes = programa.Instrucoes
                    };

                    ctx.Programado.Add(model);
                    ctx.SaveChanges();

                    retorno.Mensagem = "Programa gravado com sucesso!";
                    retorno.Mensagem = "<span style='color:green;'>" + retorno.Mensagem + "</span>";
                }
            });

            _retorno = retorno;
        }
    }
}
