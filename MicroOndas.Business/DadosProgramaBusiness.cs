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
    public class DadosProgramaBusiness : _Business
    {
        public DadosProgramaBusiness(bool logado) 
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
                    List<ProgramadoModel> dados = ctx.Programado.ToList();

                    dados.ForEach(a =>
                    {
                        this.Retorno += "<button id='" + a.Index + "' onmouseenter = 'OnMouseEnter(this);' onmouseleave = 'OnMouseLeave(this);'>" + a.Nome + "</button>";
                    });

                    this.Cor = "green";
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
