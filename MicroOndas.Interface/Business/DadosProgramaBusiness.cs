using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace MicroOndas.Interface.Business
{
    public class DadosProgramaBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration) 
        {
            RetornoAquecimentoViewModel retorno = _retorno;
            retorno.PararProgresso = false;

            LogDeErros.Log(ref retorno, () =>
            {
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {

                    List<ProgramadoModel> dados = ctx.Programado.ToList();

                    dados.ForEach(a =>
                    {
                        string style = a.Index > 5 ? "italic" : "normal";
                        retorno.Mensagem += "<div class = 'corpoprojeto'>";

                        retorno.Mensagem += "<div id='" + a.Index + "'><label style='font-style:" + style + "'>" + a.Index.ToString("00") + "</label></div>";
                        retorno.Mensagem += "<div><label style='font-style:" + style + "'> Nome: " + a.Nome + "</label></div>";
                        retorno.Mensagem += "<div><label style='font-style:" + style + "'> Alimento: " + a.Alimento + "</label></div>";
                        retorno.Mensagem += "<div><label style='font-style:" + style + "'> Tempo: " + a.Tempo.ToString("mm:ss") + "</label></div>";
                        retorno.Mensagem += "<div><label style='font-style:" + style + "'> Potencia: " + a.Potencia + "</label></div>";
                        retorno.Mensagem += "<div><label style='font-style:" + style + "'> Instruções: " + a.Intrucoes + "</label></div>";

                        retorno.Mensagem += "</div>";
                    });
                }
            });

            _retorno = retorno;
        }
    }
}
