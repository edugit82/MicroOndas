using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase.Models
{
    public class ProgramadoModel
    {
        public int Index { get; set; }
        public string? Nome { get; set; }
        public string? Alimento { get; set; }
        public DateTime Tempo { get; set; }
        public int Potencia { get; set; }
        public string? Caracter { get; set; }
        public string? Instrucoes { get; set; }
    }
}
