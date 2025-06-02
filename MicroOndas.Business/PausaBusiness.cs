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
    public class PausaBusiness : _Business
    {
        public PausaBusiness(bool logado)
        {
            VariaveisModel variaveis = new VariaveisModel();

            bool gravaerro = true;
            try
            {
                using (Context ctx = new Context())
                {
                    //Está logado
                    if (!logado)
                    {
                        this.Retorno = "Token inválido!";
                        this.Cor = "red";

                        variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                        variaveis.Valor = this.Retorno;
                        ctx.Entry(variaveis).State = EntityState.Modified;
                        ctx.SaveChanges();

                        return;
                    }

                    //Pausados
                    List<AquecimentoModel> pausados = ctx.Aquecimento.ToList().Where(a => !a.Ativo && !a.Cancelado).ToList();
                    if (pausados.Any())
                    {
                        AquecimentoModel last = pausados.OrderBy(a => a.Index).Last();

                        //Cancela
                        last.Cancelado = true;

                        ctx.Entry(last).State = EntityState.Modified;
                        ctx.SaveChanges();

                        this.Retorno = "Aquecimento cancelado!";
                        this.Cor = "red";

                        variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                        variaveis.Valor = this.Retorno;
                        ctx.Entry(variaveis).State = EntityState.Modified;
                        ctx.SaveChanges();

                        LimpaVariaveisBusiness.Limpa();
                    }

                    //Ativos
                    List<AquecimentoModel> ativos = ctx.Aquecimento.ToList().Where(a => a.Ativo && !a.Cancelado).ToList();

                    if (ativos.Any())
                    {
                        AquecimentoModel last = ativos.OrderBy(a => a.Index).Last();

                        if (last.Fim > DateTime.Now)
                        {
                            last.Pausa = DateTime.Now;
                            last.Ativo = false;

                            ctx.Entry(last).State = EntityState.Modified;
                            ctx.SaveChanges();

                            this.Retorno = "Aquecimento pausado!";
                            this.Cor = "blue";

                            variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                            variaveis.Valor = this.Retorno;
                            ctx.Entry(variaveis).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
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
