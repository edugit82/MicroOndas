using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class RetornoAquecimentoViewModel
    {
        public RetornoAquecimentoViewModel()
        {
            Token = string.Empty;
            Mensagem = string.Empty;
            PararProgresso = false;
        }

        public int Index { get; set; }
        public string Token { get; set; }
        public string Mensagem { get; set; }
        public bool PararProgresso { get; set; }
    }
    public class CadastrarViewModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string ReSenha { get; set; }
    }

    public class LogarViewModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
    public class AquecimentoViewModel
    {
        public int Id { get; set; }
        public string Tempo { get; set; }
        public string Potencia { get; set; }
    }
    public class CadastroProgramaViewModel
    {
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
}
