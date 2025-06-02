using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public static class LimpaVariaveisBusiness
    {
        public static void Limpa()
        {
            using (Context ctx = new Context())
            {
                VariaveisModel variaveis = new VariaveisModel();

                //Timer
                variaveis = ctx.Variaveis.First(a => a.Index == 1);
                variaveis.Valor = "00:00";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //Potência
                variaveis = ctx.Variaveis.First(a => a.Index == 2);
                variaveis.Valor = "01";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //Id
                variaveis = ctx.Variaveis.First(a => a.Index == 3);
                variaveis.Valor = "-1";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();                
            }
        }

        public static void LimpaCompleto() 
        {
            using (Context ctx = new Context())
            {
                VariaveisModel variaveis = new VariaveisModel();

                //Timer
                variaveis = ctx.Variaveis.First(a => a.Index == 1);
                variaveis.Valor = "00:00";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //Potência
                variaveis = ctx.Variaveis.First(a => a.Index == 2);
                variaveis.Valor = "01";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //Id
                variaveis = ctx.Variaveis.First(a => a.Index == 3);
                variaveis.Valor = "-1";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //MensagemDisplay
                variaveis = ctx.Variaveis.First(a => a.Index == 4);
                variaveis.Valor = "";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();

                //MensagemProgresso
                variaveis = ctx.Variaveis.First(a => a.Index == 5);
                variaveis.Valor = "";
                ctx.Entry(variaveis).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
