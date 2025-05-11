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
    public class AquecimentoBusiness : _Business
    {
        public AquecimentoBusiness(AquecimentoViewModel viewmodel, bool logado) 
        {
            //Está logado
            if (!logado)
            {
                this.Retorno = "Token inválido!";
                this.Cor = "red";

                return;
            }

            viewmodel.Potencia = viewmodel.Potencia ?? "10";
            viewmodel.Potencia = viewmodel.Potencia == "" ? "10" : viewmodel.Potencia;
            viewmodel.Potencia = viewmodel.Potencia == "0" ? "10" : viewmodel.Potencia;
            viewmodel.Potencia = int.Parse(viewmodel.Potencia) > 10 ? "10" : viewmodel.Potencia;

            viewmodel.Tempo = viewmodel.Tempo ?? "00:00";
            int num = 0;

            bool gravaerro = true;

            try
            {

                if (int.Parse(viewmodel.Potencia) < 1)
                    throw new AquecimentoPonteciaInvalidaException("Potência não pode ser menor que 1");

                if (viewmodel.Tempo.IndexOf(":") > -1)
                {
                    string[] split = viewmodel.Tempo.Split(':');
                    num = int.Parse(split[0]) * 60 + int.Parse(split[1]);

                    // maior que 2 minutos
                    if (num > 120)
                        throw new AquecimentoTempoExcedidoException("Tempo não pode ser maior que 2 minutos!");

                    // menor que 1 segundo
                    if (num < 1)
                        throw new AquecimentoTempoInvalidoException("Tempo não pode ser menor que 1 segundo!");
                }
                else
                {
                    num = int.Parse(viewmodel.Tempo);
                }

                using (Context ctx = new Context())
                {
                    //Elimina cancelados
                    List<AquecimentoModel> cancelados = ctx.Aquecimento.ToList().Where(a => a.Cancelado).ToList();
                    ctx.Aquecimento.RemoveRange(cancelados);
                    ctx.SaveChanges();

                    AquecimentoModel last = new AquecimentoModel();

                    //Pausados
                    List<AquecimentoModel> pausados = ctx.Aquecimento.ToList().Where(a => !a.Ativo && !a.Cancelado).ToList();

                    if (pausados.Any())
                    {
                        last = pausados.OrderBy(a => a.Index).Last();

                        //Acorda
                        last.Ativo = true;
                        TimeSpan difinicio = last.Pausa - last.Inicio;
                        TimeSpan diffim = last.Fim - last.Pausa;

                        last.Inicio = DateTime.Now;
                        last.Pausa = DateTime.Now;
                        last.Fim = last.Inicio + diffim;

                        ctx.Entry(last).State = EntityState.Modified;
                        ctx.SaveChanges();

                        return;
                    }

                    //Ativos
                    List<AquecimentoModel> ativos = ctx.Aquecimento.ToList().Where(a => a.Ativo && !a.Cancelado).ToList();
                    last = new AquecimentoModel();

                    //Novo aquecimento
                    if (!ativos.Any())
                    {
                        var model = new AquecimentoModel
                        {
                            Inicio = DateTime.Now,
                            Fim = DateTime.Now.AddSeconds(num),
                            Pausa = DateTime.Now.AddSeconds(num),
                            Potencia = int.Parse(viewmodel.Potencia),
                            Ativo = true,
                            Cancelado = false
                        };

                        ctx.Aquecimento.Add(model);
                        ctx.SaveChanges();

                        //exclui o resto
                        var resto = ctx.Aquecimento.ToList().Where(a => a.Index != model.Index).ToList();
                        ctx.Aquecimento.RemoveRange(resto);
                        ctx.SaveChanges();

                        return;
                    }

                    //Existe
                    if (ativos.Any())
                        last = ativos.OrderBy(a => a.Index).Last();

                    //Ainda em progresso, mas não pré definidos
                    if (last.Fim > DateTime.Now && (viewmodel.Id < 1 || viewmodel.Id > 5))
                    {
                        //Acrescenta o tempo
                        last.Fim = last.Fim.AddSeconds(30);

                        ctx.Entry(last).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                }
            }
            catch (LogarLoginInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (AquecimentoTempoExcedidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (AquecimentoTempoInvalidoException ex)
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
