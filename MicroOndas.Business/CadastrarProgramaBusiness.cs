using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using MicroOndas.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class CadastrarProgramaBusiness : _Business
    {
        public CadastrarProgramaBusiness(CadastroProgramaViewModel viewmodel, bool logado) 
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
                viewmodel.Nome = viewmodel.Nome.Trim();
                viewmodel.Alimento = viewmodel.Alimento.Trim();                
                viewmodel.Tempo = viewmodel.Tempo.Trim();
                viewmodel.Progresso = viewmodel.Progresso.Trim();
                viewmodel.Instrucoes = viewmodel.Instrucoes.Trim();
                viewmodel.Potencia = viewmodel.Potencia.Trim() == "" ? "0" : viewmodel.Potencia.Trim();
                viewmodel.PotenciaM = viewmodel.PotenciaM.Trim() == "" ? "0" : viewmodel.PotenciaM.Trim();

                if (viewmodel.Nome == string.Empty)
                    throw new CadastrarProgramaNomeInvalidoException("Nome do programa não pode ser vazio!");

                if (viewmodel.Alimento == string.Empty)
                    throw new CadastrarProgramaAlimentoInvalidoException("Alimento não pode ser vazio!");

                if (viewmodel.Tempo == string.Empty)
                    throw new CadastrarProgramaTempoInvalidoException("Tempo não pode ser vazio!");                

                if (viewmodel.Tempo.Length < 5)
                    throw new CadastrarProgramaTempoFormatoInvalidoException("Tempo precisa ter o formato 00:00!");         

                if (int.Parse(viewmodel.Potencia) < 1)
                    throw new CadastrarProgramaPotenciaInvalidoException("Potência não pode ser menor que 1!");

                if (viewmodel.Progresso == string.Empty)
                    throw new CadastrarProgramaProgressoInvalidoException("Progresso não pode ser vazio!");

                using (Context ctx = new Context()) 
                {
                    ProgramadoModel model = new ProgramadoModel
                    {
                        Nome = viewmodel.Nome,
                        Alimento = viewmodel.Alimento,
                        Tempo = DateTime.Parse(string.Format("01-01-1970 {0}", viewmodel.Tempo)),
                        Potencia = int.Parse(viewmodel.Potencia),
                        Caracter = viewmodel.Progresso,
                        Instrucoes = viewmodel.Instrucoes
                    };

                    ctx.Programado.Add(model);
                    ctx.SaveChanges();

                    this.Retorno = "Programa cadastrado com sucesso!";
                    this.Cor = "green";
                }
            }                        
            catch (CadastrarProgramaNomeInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarProgramaAlimentoInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarProgramaTempoInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarProgramaTempoFormatoInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarProgramaPotenciaInvalidoException ex)
            {
                this.Retorno = ex.Message;
                this.Cor = "red";
                gravaerro = false;
            }
            catch (CadastrarProgramaProgressoInvalidoException ex)
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
