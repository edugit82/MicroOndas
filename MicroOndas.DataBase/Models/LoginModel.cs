using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase.Models
{
    public class LoginModel
    {
        public int Index { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
        public string? Token { get; set; }
        public DateTime TokenTime { get; set; }
    }
}
