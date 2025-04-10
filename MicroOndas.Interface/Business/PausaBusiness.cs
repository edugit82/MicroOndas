using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using System.Data.Entity;

namespace MicroOndas.Interface.Business
{
    public class PausaBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration) 
        {
            RetornoAquecimentoViewModel retorno = _retorno;
            retorno.PararProgresso = false;

            LogDeErros.Log(ref retorno, () =>
            {
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {

                    //Pausados
                    List<AquecimentoModel> pausados = ctx.Aquecimento.ToList().Where(a => !a.Ativo && !a.Cancelado).ToList();
                    if (pausados.Any())
                    {
                        AquecimentoModel last = pausados.OrderBy(a => a.Index).Last();

                        //Cancela
                        last.Cancelado = true;

                        ctx.Entry(last).State = EntityState.Modified;
                        ctx.SaveChanges();

                        retorno.Mensagem = "<span style='color:red'>Aquecimento cancelado!</span>";
                        retorno.PararProgresso = true;
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

                            retorno.Mensagem = "<span style='color:blue'>Aquecimento pausado!</span>";

                        }

                    }
                    else
                    {
                        retorno.PararProgresso = true;
                    }

                }
            });


            _retorno = retorno;
        }
    }
}
