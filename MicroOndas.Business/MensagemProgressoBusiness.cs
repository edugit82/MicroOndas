using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class MensagemProgressoBusiness : _Business
    {
        public MensagemProgressoBusiness(bool logado)
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
                //Busca a mensagem do Mensagem
                VariaveisModel? mensagem = ctx.Variaveis.ToList().FirstOrDefault(a => a.Index == 5);
                if (mensagem is not null)
                {
                    this.Retorno = mensagem.Valor ?? "";
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
