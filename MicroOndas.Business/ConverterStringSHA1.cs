using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public static class ConverterStringSHA1
    {
        public static string Converter(string senha)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
