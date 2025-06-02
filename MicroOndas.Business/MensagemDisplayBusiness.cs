using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class MensagemDisplayBusiness : _Business
    {
        public MensagemDisplayBusiness(bool logado)
        {
            //Está logado
            if (!logado)
            {
                this.Retorno = "Token inválido!";
                this.Cor = "red";

                return;
            }

            using (Context ctx = new Context()) 
            {
                //Busca a mensagem do display
                VariaveisModel? display = ctx.Variaveis.ToList().FirstOrDefault(a => a.Index == 4);
                if (display is not null)
                {
                    this.Retorno = display.Valor ?? "";
                    this.Cor = "blue";
                }
                else
                {
                    this.Retorno = "Mensagem não encontrada!";
                    this.Cor = "red";
                }
            }
        }
    }
}
