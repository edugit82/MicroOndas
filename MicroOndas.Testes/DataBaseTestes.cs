using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using MicroOndas.Public;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MicroOndas.Testes
{
    public class DataBaseTestes
    {
        /// <summary>
        /// Teste simples, se a conexão com o Bd está funcionando.
        /// </summary>
        [Fact]
        public void TesteFuncionamentoBD() 
        {
            using (Context ctx = new Context())
            {
                List<AquecimentoModel> listaaquecimento = ctx.Aquecimento.ToList();
                List<LoginModel> listalogin = ctx.Login.ToList();
                List<ProgramadoModel> listaprogramado = ctx.Programado.ToList();
                List<VariaveisModel> listavariaveis = ctx.Variaveis.ToList();
            }
        }
        
        /// <summary>
        /// Dados Login
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<LoginModel> DadosLogin() 
        {
            yield return new LoginModel() {Usuario = "Zé das coves", Senha = "cnasheyvap" };
            yield return new LoginModel() {Usuario = "Zé das coves01", Senha = "cnasheyvap" };
            yield return new LoginModel() {Usuario = "Zé das coves02", Senha = "cnasheyvap" };
        }

        /// <summary>
        /// Teste de cadastro de login, insere dados na tabela Login.
        /// </summary>
        [Fact]        
        public void CadastraLogin() 
        {            
            List<LoginModel> dados = DadosLogin().ToList();

            using (Context ctx = new Context())
            {
                //Limpa tabela
                ctx.Database.ExecuteSqlRaw("truncate table Login");
                ctx.SaveChanges();

                //Insere dados
                dados.ForEach(a =>
                {
                    try
                    {
                        a.Index = 0; //Limpa o index para não dar erro de duplicidade
                        ctx.Login.Add(a);
                        ctx.SaveChanges();

                        Debug.WriteLine("Dado inserido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                    }
                });
            }
        }
        
        /// <summary>
        /// Teste de alteração de login, altera dados na tabela Login.
        /// </summary>
        [Fact]
        public void AlteraLogin() 
        {
            using (Context ctx = new Context())
            {
                List<LoginModel> dados = ctx.Login.ToList();

                if (!dados.Any())
                    return;

                dados.ForEach(a => 
                {
                    a.Senha = "12345";

                    //Altera dados                    
                    ctx.Entry(a).State = EntityState.Modified;
                    ctx.SaveChanges();

                    Debug.WriteLine(string.Format("Index: {0}: Dado alterado com sucesso!", a.Index));

                });                
            }
        }

        /// <summary>
        /// Dados Aquecimento
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<AquecimentoModel> DadosAquecimento()
        {
            string dia = DateTime.Now.ToString("dd-MM-yyyy ");

            Func<string,DateTime> geratempo = (hora) => 
            {
                return DateTime.Parse(dia + hora);
            };

            AquecimentoModel modelo = new AquecimentoModel()
            {                
                Inicio = geratempo("00:00:00"),
                Fim = geratempo("12:00:00"),
                Pausa = geratempo("07:00:00"),
                Ativo = true,
                Cancelado = false,
                Potencia = 1
            };

            yield return modelo;
            yield return modelo;
            yield return modelo;
        }

        /// <summary>
        /// Teste de cadastro de aquecimento, insere dados na tabela Aquecimento.
        /// </summary>
        [Fact]
        public void CadastraAquecimento()
        {
            List<AquecimentoModel> dados = DadosAquecimento().ToList();

            using (Context ctx = new Context())
            {
                //Limpa tabela
                ctx.Database.ExecuteSqlRaw("truncate table Aquecimento");
                ctx.SaveChanges();

                //Insere dados
                dados.ForEach(a =>
                {
                    try
                    {                        
                        a.Index = 0; //Limpa o index para não dar erro de duplicidade
                        ctx.Aquecimento.Add(a);
                        ctx.SaveChanges();

                        Debug.WriteLine("Dado inserido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                    }
                });
            }
        }

        /// <summary>
        /// Teste de alteração de aquecimento, altera dados na tabela Aquecimento.
        /// </summary>
        [Fact]
        public void AlteraAquecimento()
        {
            using (Context ctx = new Context())
            {
                List<AquecimentoModel> dados = ctx.Aquecimento.ToList();

                if (!dados.Any())
                    return;

                dados.ForEach(a =>
                {
                    try
                    {
                        int index = dados.FindIndex(b => b.Index == a.Index);

                        a.Inicio = a.Inicio.AddHours(index);
                        a.Pausa = a.Pausa.AddHours(index);
                        a.Fim = a.Fim.AddHours(index);

                        //Altera dados                    
                        ctx.Entry(a).State = EntityState.Modified;
                        ctx.SaveChanges();

                        Debug.WriteLine(string.Format("Index: {0}: Dado alterado com sucesso!", a.Index));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                    }
                });
            }
        }

        /// <summary>
        /// Dados Programado
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProgramadoModel> DadosProgramado()
        {
            ProgramadoModel modelo = new ProgramadoModel()
            {
                Nome = "Teste",
                Alimento = "Arroz",
                Tempo = DateTime.Now,
                Potencia = 1,
                Caracter = ".",
                Instrucoes = "Coloque o arroz na tigela e adicione água."
            };

            yield return modelo;
            yield return modelo;
            yield return modelo;
        }
        
        /// <summary>
        /// Teste de cadastro de programado, insere dados na tabela Programado.
        /// </summary>
        [Fact]
        public void CadastraProgramado()
        {
            List<ProgramadoModel> dados = DadosProgramado().ToList();

            using (Context ctx = new Context())
            {
                //Limpa tabela
                ctx.Database.ExecuteSqlRaw("truncate table Programado");
                ctx.SaveChanges();

                //Insere dados
                dados.ForEach(a =>
                {
                    try
                    {
                        a.Index = 0; //Limpa o index para não dar erro de duplicidade
                        ctx.Programado.Add(a);
                        ctx.SaveChanges();

                        Debug.WriteLine("Dado inserido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                    }
                });
            }
        }

        /// <summary>
        /// Teste de alteração de programado, altera dados na tabela Programado.
        /// </summary>
        [Fact]
        public void AlteraProgramado()
        {
            using (Context ctx = new Context())
            {
                List<ProgramadoModel> dados = ctx.Programado.ToList();

                if (!dados.Any())
                    return;

                dados.ForEach(a =>
                {
                    try
                    {
                        a.Instrucoes = "Coloque o arroz na tigela e adicione água. Depois coloque no microondas e ligue por 10 minutos.";

                        //Altera dados                    
                        ctx.Entry(a).State = EntityState.Modified;
                        ctx.SaveChanges();

                        Debug.WriteLine(string.Format("Index: {0}: Dado alterado com sucesso!", a.Index));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                    }
                });
            }
        }
        /// <summary>
        /// Teste de cadastro de programado, insere dados na tabela Programado.
        /// </summary>
        [Fact]
        public void FeedReceitasProgramadas()
        {
            using (Context ctx = new Context())
            {
                //Remove todos
                ctx.Database.ExecuteSqlRaw("truncate table Programado");
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Pipoca",
                    Alimento = "Pipoca",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(3),
                    Potencia = 7,
                    Caracter = ".",
                    Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."

                });
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Leite",
                    Alimento = "Leite",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(5),
                    Potencia = 5,
                    Caracter = ".",
                    Instrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."

                });
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Carnes de boi",
                    Alimento = "Carne em pedaço ou fatias",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(14),
                    Potencia = 4,
                    Caracter = ".",
                    Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."

                });
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Frango",
                    Alimento = "Frango qualquer corte",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(8),
                    Potencia = 7,
                    Caracter = ".",
                    Instrucoes = "Interrompa Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."

                });
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Feijão",
                    Alimento = "Feijão congelado",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(8),
                    Potencia = 9,
                    Caracter = ".",
                    Instrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."

                });
                ctx.SaveChanges();

                ctx.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Ovo",
                    Alimento = "Ovo frito",
                    Tempo = DateTime.Parse("01-01-1970 00:01:19"),
                    Potencia = 1,
                    Caracter = "$",
                    Instrucoes = "Ovo frito é acompanhamento á diversos pratos."

                });
                ctx.SaveChanges();
            }
        }
        /// <summary>
        /// Teste de cadastro de variaveis, insere dados na tabela Variaveis.
        /// </summary>
        [Fact]
        public void FeedVariaveis()
        {
            using (Context ctx = new Context())
            {
                //Remove todos
                ctx.Database.ExecuteSqlRaw("truncate table Variaveis");
                ctx.SaveChanges();
                ctx.Variaveis.Add(new VariaveisModel()
                {
                    Descricao = "Timer",
                    Valor = "00:00"
                });
                ctx.SaveChanges();
                ctx.Variaveis.Add(new VariaveisModel()
                {
                    Descricao = "Potencia",
                    Valor = "01"
                });
                ctx.SaveChanges();
                ctx.Variaveis.Add(new VariaveisModel()
                {
                    Descricao = "Id",
                    Valor = "-1"
                });
                ctx.SaveChanges();
                ctx.Variaveis.Add(new VariaveisModel()
                {
                    Descricao = "MensagemDisplay",
                    Valor = ""
                });
                ctx.SaveChanges();
                ctx.Variaveis.Add(new VariaveisModel()
                {
                    Descricao = "MensagemProgresso",
                    Valor = ""
                });
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Teste de cadastro de variaveis, insere dados na tabela Variaveis.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> VariaveisDados()
        {
            yield return new object[] { 1, "Timer", "00:00" };
            yield return new object[] { 2, "Potencia", "10" };
            yield return new object[] { 3, "Id", "0" };
            yield return new object[] { 3, "MensagemDisplay", "" };
            yield return new object[] { 3, "MensagemProgresso", "" };
        }

        /// <summary>
        /// Teste Limpa tabela Variaveis
        /// </summary>
        [Fact]
        public void LimpaVariaveis()
        {
            using (Context ctx = new Context())
            {
                //Limpa tabela
                ctx.Database.ExecuteSqlRaw("truncate table Variaveis");
                ctx.SaveChanges();
            }
        }

        [Theory]
        [MemberData(nameof(VariaveisDados))]
        public void CadastraVariaveis(string nome, string valor)
        {            
            using (Context ctx = new Context())
            {                
                //Insere dados
                try
                {
                    ctx.Variaveis.Add(new VariaveisModel()
                    {                        
                        Descricao = nome,
                        Valor = valor
                    });
                    ctx.SaveChanges();
                    Debug.WriteLine("Dado inserido com sucesso!");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Mensagem: {0}", ex.Message));
                }
            }
        }
    }
}
