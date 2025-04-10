using MicroOndas.Business;
using MicroOndas.DataBase.Models;
using MicroOndas.DataBase;
using System.Data.Entity;

namespace MicroOndas.Interface.Business
{
    public class ProgressoBusiness
    {
        public void Business(ref RetornoAquecimentoViewModel _retorno, IConfiguration _configuration, ProgressoViewModel progresso) 
        {
            RetornoAquecimentoViewModel retorno = _retorno;

            LogDeErros.Log(ref retorno, () =>
            {
                string conn = _configuration["Conn"] ?? "";
                conn = CryptoHelper.Decrypt(conn);

                using (Context ctx = new Context(conn))
                {
                    //Ativos
                    List<AquecimentoModel> ativos = ctx.Aquecimento.ToList().Where(a => a.Ativo == true && !a.Cancelado).ToList();

                    //Valida se Existe regitro
                    bool exist = ativos.Any();

                    //Existe
                    if (exist)
                    {
                        string caracter = ".";
                        //Pega o caracter de carregamento
                        if (progresso.Id > -1)
                        {
                            ProgramadoModel? programado = ctx.Programado.ToList().Where(a => a.Index == progresso.Id).FirstOrDefault();
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
                                retorno.Mensagem += spontencia + " ";

                            retorno.Mensagem = "<span style='color:blue;'>" + retorno.Mensagem + "</span>";
                            return;
                        }
                        else
                        {
                            last.Ativo = false;
                            last.Cancelado = true;

                            ctx.Entry(last).State = EntityState.Modified;
                            ctx.SaveChanges();

                            retorno.Mensagem = "<span style='color:green'>Aquecimento finalizado!</span>";
                            return;
                        }
                    }

                    //Pausados
                    List<AquecimentoModel> pausados = ctx.Aquecimento.ToList().Where(a => !a.Ativo && !a.Cancelado).ToList();

                    exist = pausados.Any();

                    if (exist)
                    {
                        retorno.Mensagem = "<span style='blue'>Aquecimento pausado!</span>";
                    }
                }
            });

            _retorno = retorno;
        }
    }
}
