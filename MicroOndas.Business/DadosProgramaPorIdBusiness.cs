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
    public class DadosProgramaPorIdBusiness
    {        
        public DadosProgramaPorIdViewModel? Retorno { get; set; }
        public DadosProgramaPorIdBusiness(DadosProgramaPorIdViewModel viewmodel) 
        {            

            bool gravaerro = true;
            try
            {
                using (Context ctx = new Context())
                {
                    ProgramadoModel? programa = ctx.Programado.FirstOrDefault(a => a.Index == viewmodel.Id);

                    if (programa is not null)
                        this.Retorno = new DadosProgramaPorIdViewModel()
                        {
                            Id = viewmodel.Id,
                            Tempo = programa is not null ? programa.Tempo.Minute.ToString("00") + ":" + programa.Tempo.Second.ToString("00") : "00:00",
                            Potencia = programa is not null ? programa.Potencia : 0
                        };
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
