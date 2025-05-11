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
    public class ProgressoBusiness : _Business
    {
        public ProgressoBusiness(ProgressoViewModel viewmodel, bool logado)
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
                    //Ativos
                    List<AquecimentoModel> ativos = ctx.Aquecimento.ToList().Where(a => a.Ativo && !a.Cancelado).ToList();

                    //Valida se Existe regitro
                    bool exist = ativos.Any();

                    //Existe
                    if (exist)
                    {
                        string caracter = ".";
                        //Pega o caracter de carregamento
                        if (viewmodel.Id > -1)
                        {
                            ProgramadoModel? programado = ctx.Programado.ToList().Where(a => a.Index == viewmodel.Id).FirstOrDefault();
                            caracter = programado?.Caracter ?? caracter;
                        }

                        AquecimentoModel last = ativos.OrderBy(a => a.Index).Last();
                        //Ainda em progresso
                        if (last.Fim > DateTime.Now)
                        {
                            //Tempo faltando
                            int faltando = (int)(last.Fim - DateTime.Now).TotalSeconds;

                            //Intervalos
                            int intervalos = (int)(faltando / last.Potencia);

                            //Potencia
                            string spontencia = "";
                            for (var i = 0; i < last.Potencia; i++)
                                spontencia += caracter;

                            //Monta imagem progresso
                            for (var i = 0; i < intervalos; i++)
                                this.Retorno += spontencia + " ";

                            this.Cor = "yellow";
                            return;
                        }
                        else
                        {
                            last.Ativo = false;
                            last.Cancelado = true;

                            ctx.Entry(last).State = EntityState.Modified;
                            ctx.SaveChanges();

                            this.Retorno = "Aquecimento finalizado!";
                            this.Cor = "green";

                            return;
                        }                        
                    }

                    //Pausados
                    List<AquecimentoModel> pausados = ctx.Aquecimento.ToList().Where(a => !a.Ativo && !a.Cancelado).ToList();

                    exist = pausados.Any();

                    if (exist)
                    {
                        this.Retorno = "Aquecimento pausado!";
                        this.Cor = "blue";
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
