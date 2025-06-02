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
        public ProgressoBusiness(bool logado)
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

                    //Ativos
                    List<AquecimentoModel> ativos = ctx.Aquecimento.ToList().Where(a => a.Ativo && !a.Cancelado).ToList();

                    //Valida se Existe regitro
                    bool exist = ativos.Any();

                    //Existe
                    if (exist)
                    {
                        VariaveisModel? idmodel = ctx.Variaveis.FirstOrDefault(a => a.Index == 3);
                        int id = -1;
                        int.TryParse(idmodel?.Valor, out id);
                        string caracter = "."; //Caracter de carregamento padrão

                        //Pega o caracter de carregamento
                        if (id > -1)
                        {
                            ProgramadoModel? programado = ctx.Programado.ToList().Where(a => a.Index == id).FirstOrDefault();
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

                            variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                            variaveis.Valor = this.Retorno;
                            ctx.Entry(variaveis).State = EntityState.Modified;
                            ctx.SaveChanges();

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

                            variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                            variaveis.Valor = this.Retorno;
                            ctx.Entry(variaveis).State = EntityState.Modified;
                            ctx.SaveChanges();

                            LimpaVariaveisBusiness.Limpa();

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

                        variaveis = ctx.Variaveis.ToList().First(a => a.Index == 5);
                        variaveis.Valor = this.Retorno;
                        ctx.Entry(variaveis).State = EntityState.Modified;
                        ctx.SaveChanges();
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
