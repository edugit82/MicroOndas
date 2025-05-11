using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using MicroOndas.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class DescricaoProgramaBusiness : _Business
    {
        public DescricaoProgramaBusiness(DescricaoProgramaViewModel viewmodel,bool logado)
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
                using (Context ctx = new Context())
                {
                    ProgramadoModel? programa = ctx.Programado.FirstOrDefault(a => a.Index == viewmodel.Id);
                    if (programa is null) 
                    {
                        this.Retorno = "<span style='color:red;'>Programa não encontrado.</span>";
                    }
                    else
                    {
                        string style = programa.Index > 5 ? "italic" : "normal";
                        this.Retorno += "<div class = 'corpoprojeto'>";

                        this.Retorno += "<div id='" + programa.Index + "'><label style='font-style:" + style + "'>" + programa.Index.ToString("00") + "</label></div>";
                        this.Retorno += "<div><label style='font-style:" + style + "'> Nome: " + programa.Nome + "</label></div>";
                        this.Retorno += "<div><label style='font-style:" + style + "'> Alimento: " + programa.Alimento + "</label></div>";
                        this.Retorno += "<div><label style='font-style:" + style + "'> Tempo: " + programa.Tempo.ToString("mm:ss") + "</label></div>";
                        this.Retorno += "<div><label style='font-style:" + style + "'> Potencia: " + programa.Potencia + "</label></div>";
                        this.Retorno += "<div><label style='font-style:" + style + "'> Instruções: " + programa.Instrucoes + "</label></div>";

                        this.Retorno += "</div>";                        
                    }                    
                }
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
