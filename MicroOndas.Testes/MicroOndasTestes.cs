using MicroOndas.Business;
using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MicroOndas.Testes
{
    [TestClass]
    public class MicroOndasTestes
    {
        string path = @"C:\Users\user\source\repos\MicroOndas\MicroOndas.Interface\bin\Debug\net9.0";
        IConfiguration configuration;

        private void IniciaConfig() 
        {
            configuration = new ConfigurationBuilder()
            .SetBasePath(path) // Define o diretório base
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Adiciona o arquivo JSON
            .AddEnvironmentVariables() // Adiciona variáveis de ambiente
            .Build();
        } 
        
        /// <summary>
        /// Criar as receitas padrão do micro ondas
        /// </summary>
        
        [TestMethod]
        public void CriarReceitasProgramadas()
        {
            IniciaConfig();
            
            string conn = configuration["Conn"] ?? "";
            conn = CryptoHelper.Decrypt(conn);

            using (var context = new Context(conn))
            {
                //Remove todos
                context.Database.ExecuteSqlCommand("truncate table Programado");
                context.SaveChanges();

                context.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Pipoca",
                    Alimento = "Pipoca",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(3),
                    Potencia = 7,
                    Caracter = ".",
                    Intrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."

                });
                context.SaveChanges();

                context.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Leite",
                    Alimento = "Leite",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(5),
                    Potencia = 5,
                    Caracter = ".",
                    Intrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."

                });
                context.SaveChanges();

                context.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Carnes de boi",
                    Alimento = "Carne em pedaço ou fatias",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(14),
                    Potencia = 4,
                    Caracter = ".",
                    Intrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."

                });
                context.SaveChanges();

                context.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Frango",
                    Alimento = "Frango qualquer corte",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(8),
                    Potencia = 7,
                    Caracter = ".",
                    Intrucoes = "Interrompa Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."

                });
                context.SaveChanges();

                context.Programado.Add(new ProgramadoModel()
                {
                    Nome = "Feijão",
                    Alimento = "Feijão congelado",
                    Tempo = DateTime.Parse("01-01-1970 00:00:00") + TimeSpan.FromMinutes(8),
                    Potencia = 9,
                    Caracter = ".",
                    Intrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."

                });
                context.SaveChanges();
            }
        }
    }
}
