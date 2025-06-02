using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using MicroOndas.Public;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MicroOndas.Business
{
    public class BotaoTextoBusiness : _Business
    {
        public BotaoTextoBusiness(BotaoTextoViewModel viewmodel, bool logado)
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
                    VariaveisModel? model = ctx.Variaveis.ToList().FirstOrDefault(a => a.Index == viewmodel.Tipo);
                    if (model is not null)
                    {
                        // Criar objeto Regex
                        Regex regex = new Regex(@"[0-9]");                        

                        Func<MatchCollection,int, string> matchnull = (_matches,index) => 
                        {
                            string retorno = "0";

                            try
                            {
                                retorno = _matches[index].Value;
                            }
                            catch 
                            { 
                            }
                            
                            return retorno;
                        };

                        //Timer                        
                        if (model.Index == 1) 
                        {
                            // Adicionar o texto ao valor atual
                            model.Valor += viewmodel.Texto;

                            // Encontrar todas as correspondências
                            MatchCollection matches = regex.Matches(model.Valor ?? "");

                            int matchlength = matches.Count;
                            string _01 = matchnull(matches, matchlength - 4);
                            string _02 = matchnull(matches, matchlength - 3);
                            string _03 = matchnull(matches, matchlength - 2);
                            string _04 = matchnull(matches, matchlength - 1);

                            model.Valor = string.Format("{0}{1}:{2}{3}",_01, _02, _03, _04);

                            ctx.Entry(model).State = EntityState.Modified;
                            ctx.SaveChanges();

                            VariaveisModel _model = ctx.Variaveis.ToList().First(a => a.Index == 4);
                            _model.Valor = model.Valor;
                            ctx.Entry(_model).State = EntityState.Modified;
                            ctx.SaveChanges();

                        }

                        //Potencia
                        if (model.Index == 2) 
                        {
                            int num = 0;
                            int.TryParse(model.Valor?.Trim(), out num);

                            // valor a variavel
                            if (viewmodel.Texto?.Trim() == "+")                             
                                num += 1;

                            //  reduz valor da variavel
                            if (viewmodel.Texto?.Trim() == "-")
                                num -= 1;

                            //Valida o intervalo da potencia
                            if (num < 1 || num > 10)
                                num = 10;

                            model.Valor = num.ToString();

                            ctx.Entry(model).State = EntityState.Modified;
                            ctx.SaveChanges();

                            VariaveisModel _model = ctx.Variaveis.ToList().First(a => a.Index == 4);
                            _model.Valor = model.Valor;
                            ctx.Entry(_model).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }

                        //Texto
                        this.Retorno = model.Valor ?? "";
                        this.Cor = "green";
                    }
                    else
                    {
                        this.Retorno = "Variável não existe!";
                        this.Cor = "red";
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
