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
            Senha = string.Empty;
            ReSenha = string.Empty;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public string ReSenha { get; set; }
    }

    public class LogarViewModel
    {
        public LogarViewModel()
        {
            Login = string.Empty;
            Senha = string.Empty;
        }
        public string Login { get; set; }
        public string Senha { get; set; }
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
            Alimento = string.Empty;
            Tempo = string.Empty;
            Potencia = 0;
            Progresso = string.Empty;
            Instrucoes = string.Empty;
        }
        public string Nome { get; set; }
        public string Alimento { get; set; }
        public string Tempo { get; set; }
        public int Potencia { get; set; }
        public string Progresso { get; set; }
        public string Instrucoes { get; set; }
    }
    public class ProgressoViewModel
    {
        public int Id { get; set; }
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
}