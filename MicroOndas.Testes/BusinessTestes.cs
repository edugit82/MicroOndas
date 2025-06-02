using MicroOndas.Business;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Testes
{
    public class BusinessTestes
    {
        /// <summary>
        /// Limpa o banco de dados para os testes.
        /// </summary>
        [Fact]
        public void ZeraBanco()
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Login");
                ctx.SaveChanges();

                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();

                ctx.Database.ExecuteSqlRaw("truncate table Programado");
                ctx.SaveChanges();
            }
        }
        /// <summary>
        /// Dados de teste para o cadastro de usuário.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> CadastroData()
        {   
            yield return new object[] { "", "1234", "1234" };
            yield return new object[] { "Eduardo", "", "1234" };
            yield return new object[] { "Eduardo", "1234", "" };
            yield return new object[] { "Eduardo", "1234", "4321" };
            yield return new object[] { "Eduardo", "1234", "1234" };
        }

        /// <summary>
        /// Testa o cadastro de usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="confirma"></param>
        [Theory]
        [MemberData(nameof(CadastroData))]
        public void TestCadastro(string login, string senha, string confirma)
        {
            CadastrarViewModel model = new CadastrarViewModel
            {
                Login = login,
                Senha = senha,
                Confirma = confirma
            };

            CadastrarBusiness cadastroBusiness = new CadastrarBusiness(model, true);
        }

        /// <summary>
        /// Dados de teste para o login de usuário.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> LogarData()
        {
            yield return new object[] { "", "" };
            yield return new object[] { "Eduardo", "" };
            yield return new object[] { "JoãoSilva", "4321" };
            yield return new object[] { "João", "4321" };
            yield return new object[] { "Eduardo", "1234" };
        }

        /// <summary>
        /// Testa o login de usuário.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        [Theory]
        [MemberData(nameof(LogarData))]
        public void TestLogin(string login, string senha)
        {
            LogarViewModel model = new LogarViewModel
            {
                Login = login,
                Senha = senha                
            };

            LogarBusiness logarbusiness = new LogarBusiness(model, true);
        }

        /// <summary>
        /// Dados de teste para o aquecimento.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> AquecimentoData()
        {
            yield return new object[] { 1, "", "" };
            yield return new object[] { 1, "00:00", "10" };
            yield return new object[] { 1, "03:00", "1" };
        }

        /// <summary>
        /// Testa o aquecimento.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        [Theory]
        [MemberData(nameof(AquecimentoData))]
        public void TestAquecimento(int id, string tempo, string potencia)
        {
            AquecimentoViewModel model = new AquecimentoViewModel()
            {
                Id = id,
                Tempo = tempo,
                Potencia = potencia
            };

            AquecimentoBusiness aquecimentobusiness = new AquecimentoBusiness(model,true);
        }

        /// <summary>
        /// Testa o aquecimento pausado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        [Theory]
        [InlineData(1, "01:00", "5")]
        public void TesteAquecimentoPausado(int id, string tempo, string potencia)
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now - TimeSpan.FromHours(1);

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora + TimeSpan.FromMinutes(1),
                Potencia = 5,
                Ativo = false,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            AquecimentoViewModel model = new AquecimentoViewModel()
            {
                Id = id,
                Tempo = tempo,
                Potencia = potencia
            };
            AquecimentoBusiness aquecimentobusiness = new AquecimentoBusiness(model,true);

            using (Context ctx = new Context())
            {
                List<AquecimentoModel> aquecimentos = ctx.Aquecimento.ToList();
            }
        }

        /// <summary>
        /// Cria novo aquecimento.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        [Theory]
        [InlineData(1, "01:00", "5")]
        public void TesteAquecimentoNovo(int id, string tempo, string potencia)
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }
            AquecimentoViewModel model = new AquecimentoViewModel()
            {
                Id = id,
                Tempo = tempo,
                Potencia = potencia
            };
            AquecimentoBusiness aquecimentobusiness = new AquecimentoBusiness(model, true);
            using (Context ctx = new Context())
            {
                List<AquecimentoModel> aquecimentos = ctx.Aquecimento.ToList();
            }
        }

        /// <summary>
        /// Acrescenta tempo ao aquecimento em andamento.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        [Theory]
        [InlineData(6, "01:19", "7")]
        public void TesteAquecimentoSomaTempo(int id, string tempo, string potencia) 
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now;

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora,
                Potencia = 5,
                Ativo = true,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            AquecimentoViewModel model = new AquecimentoViewModel()
            {
                Id = id,
                Tempo = tempo,
                Potencia = potencia
            };
            AquecimentoBusiness aquecimentobusiness = new AquecimentoBusiness(model, true);

            using (Context ctx = new Context())
            {
                List<AquecimentoModel> dados = ctx.Aquecimento.ToList();
            }
        }

        /// <summary>
        /// Testa o progresso do aquecimento.
        /// </summary>        
        [Fact]        
        public void TesteAquecimentoValidaProgresso() 
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now;

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora,
                Potencia = 5,
                Ativo = true,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            for (var i = 0; i < (60 * 3); i++) 
            {
                ProgressoBusiness progressoBusiness = new ProgressoBusiness(true);
                Debug.WriteLine(string.Format("Cor: {0}, Progresso: {1}", progressoBusiness.Cor, progressoBusiness.Retorno));
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Testa o progresso do aquecimento pausado.
        /// </summary>        
        [Fact]        
        public void TesteAquecimentoPausaProgresso()
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now;

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora,
                Potencia = 5,
                Ativo = false,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            for (var i = 0; i < (60 * 3); i++)
            {
                ProgressoBusiness progressoBusiness = new ProgressoBusiness(true);
                Debug.WriteLine(string.Format("Cor: {0}, Progresso: {1}", progressoBusiness.Cor, progressoBusiness.Retorno));
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Testa cancelamento ao clicar no botão pausa.
        /// </summary>
        [Fact]
        public void PausaCancelados() 
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now;

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora,
                Potencia = 5,
                Ativo = false,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            PausaBusiness pausaBusiness = new PausaBusiness(true);
        }

        /// <summary>
        /// Testa pausa do aquecimento em progresso.
        /// </summary>
        [Fact]
        public void PausaPusados()
        {
            using (Context ctx = new Context())
            {
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();
            }

            DateTime hora = DateTime.Now;

            AquecimentoModel aquecimentomodel = new AquecimentoModel
            {
                Index = 0,
                Inicio = hora,
                Fim = hora + TimeSpan.FromMinutes(2),
                Pausa = hora,
                Potencia = 5,
                Ativo = true,
                Cancelado = false
            };

            using (Context ctx = new Context())
            {
                ctx.Aquecimento.Add(aquecimentomodel);
                ctx.SaveChanges();
            }

            PausaBusiness pausaBusiness = new PausaBusiness(true);
        }

        /// <summary>
        /// Dados de teste para o cadastro de programa.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> ProgramaData()
        {
            yield return new object[] { "", "", "", "0", "", "" };
            yield return new object[] { "Frango Frito", "", "", "0", "", "" };
            yield return new object[] { "Frango Frito", "Frango", "", "0", "", "" };
            yield return new object[] { "Frango Frito", "Frango", "01:15", "0", "", "" };
            yield return new object[] { "Frango Frito", "Frango", "01:15", "5", "", "" };
            yield return new object[] { "Frango Frito", "Frango", "01:15", "5", "$", "Ótimo prato, acompanhado com batata!" };
        }

        /// <summary>
        /// Testa o cadastro de programa.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="alimento"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        /// <param name="progresso"></param>
        /// <param name="instrucoes"></param>
        [Theory]
        [MemberData(nameof(ProgramaData))]
        public void TesteCadastroPrograma(string nome, string alimento, string tempo, string potencia, string progresso, string instrucoes)
        {
            CadastroProgramaViewModel model = new CadastroProgramaViewModel
            {
                Nome = nome,
                Alimento = alimento,
                Tempo = tempo,
                Potencia = potencia,
                Progresso = progresso,
                Instrucoes = instrucoes
            };
            CadastrarProgramaBusiness cadastrarprogramabusiness = new CadastrarProgramaBusiness(model, true);
        }

        /// <summary>
        /// Gera dados de teste para o programa.
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="alimento"></param>
        /// <param name="tempo"></param>
        /// <param name="potencia"></param>
        /// <param name="progresso"></param>
        /// <param name="instrucoes"></param>
        [Theory]
        [InlineData("Frango Frito", "Frango", "01:15", 5, "$", "Ótimo prato, acompanhado com batata!")]
        public void TesteDadosPrograma(string nome, string alimento, string tempo, int potencia, string progresso, string instrucoes)
        {
            ProgramadoModel model = new ProgramadoModel
            {
                Nome = nome,
                Alimento = alimento,
                Tempo = DateTime.Parse(string.Format("01-01-1970 00:{0}", tempo)),
                Potencia = potencia,
                Caracter = progresso,
                Instrucoes = instrucoes
            };            

            using (Context ctx = new Context())
            {
                ctx.Programado.Add(model);
                ctx.SaveChanges();
            }

            DadosProgramaBusiness dadosprogramabusiness = new DadosProgramaBusiness(true);
        }

        /// <summary>
        /// Testa o botão de texto.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="valor"></param>
        [Theory]
        [InlineData(1, "0")]
        [InlineData(1, "1")]
        [InlineData(1, "2")]
        [InlineData(2, "+")]
        [InlineData(2, "-")]
        public void TesteBotaoTexto(int index, string valor) 
        {
            BotaoTextoViewModel viewmodel = new BotaoTextoViewModel()
            {
                Tipo = index,
                Texto = valor
            };

            BotaoTextoBusiness botaotextobusiness = new BotaoTextoBusiness(viewmodel, true);
        }

        /// <summary>
        /// Testa a obtenção de programas agendados.
        /// </summary>
        [Fact]
        public void TesteGetProgramado() 
        {
            GetProgramas getTituloProgramas = new GetProgramas();
        }
    }
}
