using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{    
    public class CadastrarViewModel
    {
        public CadastrarViewModel()
        {            
            Login = string.Empty;
            LoginM = string.Empty;
            Senha = string.Empty;
            SenhaM = string.Empty;
            Confirma = string.Empty;
            ConfirmaM = string.Empty;
        }

        public string Login { get; set; }
        public string LoginM { get; set; }
        public string Senha { get; set; }
        public string SenhaM { get; set; }
        public string Confirma { get; set; }
        public string ConfirmaM { get; set; }
    }

    public class LogarViewModel
    {
        public LogarViewModel()
        {
            Login = string.Empty;
            LoginM = string.Empty;
            Senha = string.Empty;
            SenhaM = string.Empty;
        }
        public string Login { get; set; }
        public string LoginM { get; set; }
        public string Senha { get; set; }
        public string SenhaM { get; set; }
    }
    public class AquecimentoViewModel
    {
        public AquecimentoViewModel()
        {            
            Tempo = string.Empty;
            Potencia = string.Empty;
        }
        public int Id { get; set; }
        public string Tempo { get; set; }
        public string Potencia { get; set; }
    }
    public class CadastroProgramaViewModel
    {
        public CadastroProgramaViewModel()
        {
            Nome = string.Empty;
            NomeM = string.Empty;
            Alimento = string.Empty;
            AlimentoM = string.Empty;
            Tempo = string.Empty;
            TempoM = string.Empty;
            Potencia = string.Empty;
            PotenciaM = string.Empty;
            Progresso = string.Empty;
            ProgressoM = string.Empty;
            Instrucoes = string.Empty;
            InstrucoesM = string.Empty;
        }
        public string Nome { get; set; }
        public string NomeM { get; set; }
        public string Alimento { get; set; }
        public string AlimentoM { get; set; }
        public string Tempo { get; set; }
        public string TempoM { get; set; }
        public string Potencia { get; set; }
        public string PotenciaM { get; set; }
        public string Progresso { get; set; }
        public string ProgressoM { get; set; }
        public string Instrucoes { get; set; }
        public string InstrucoesM { get; set; }
    }    
    public class DescricaoProgramaViewModel
    {
        public int Id { get; set; }
    }
    public class DadosProgramaPorIdViewModel
    {
        public int Id { get; set; }
        public string? Tempo { get; set; }
        public int Potencia { get; set; }
    }
    public class BotaoTextoViewModel 
    {
        public int Tipo { get; set; }
        public string? Texto { get; set; }
    }
}