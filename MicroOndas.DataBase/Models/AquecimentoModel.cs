using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase.Models
{
    public class AquecimentoModel
    {
        public int Index { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Pausa { get; set; }
        public int Potencia { get; set; }
        public bool Ativo { get; set; }
        public bool Cancelado { get; set; }
    }
}
