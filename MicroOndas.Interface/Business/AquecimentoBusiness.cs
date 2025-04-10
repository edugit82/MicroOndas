using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using System.Data.Entity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MicroOndas.Interface.Business
{
    public class AquecimentoBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration, AquecimentoViewModel aquecimento)
        {
            RetornoAquecimentoViewModel retorno = _retorno;

            LogDeErros.Log(ref retorno, () =>
            {
                aquecimento.Potencia = aquecimento.Potencia ?? "10";
                aquecimento.Potencia = aquecimento.Potencia  == "0" ? "10" : aquecimento.Potencia;
                aquecimento.Potencia = int.Parse(aquecimento.Potencia) > 10  ? "10" : aquecimento.Potencia;

                aquecimento.Tempo = aquecimento.Tempo ?? "00:00";
                int num = 0;

                if (int.Parse(aquecimento.Potencia) < 1)
                {
                    retorno.Mensagem = "<span style='color:red;'>Potência não pode ser menor que 1!</span>";
                    return;
                }

                if (aquecimento.Tempo.IndexOf(":") > -1)
                {
                    string[] split = aquecimento.Tempo.Split(':');
                    num = int.Parse(split[0]) * 60 + int.Parse(split[1]);

                    // maior que 2 minutos
                    if (num > 120)
                    {
                        retorno.Mensagem = "<span style='color:red;'>Tempo não pode ser maior que 2 minutos!</span>";
                        return;
                    }

                    // menor que 1 segundo
                    if (num < 1)
                    {
                        retorno.Mensagem = "<span style='color:red;'>Tempo não pode ser menor que 1 segundo!</span>";
                        return;
                    }
                }
                else
                {
                    num = int.Parse(aquecimento.Tempo);
                }

                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
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
                            Potencia = int.Parse(aquecimento.Potencia),
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
                    if (last.Fim > DateTime.Now && (aquecimento.Id < 1 || aquecimento.Id > 5))
                    {
                        //Acrescenta o tempo
                        last.Fim = last.Fim.AddSeconds(30);

                        ctx.Entry(last).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }                    
                }

            });

            _retorno = retorno;
        }
    }
}
