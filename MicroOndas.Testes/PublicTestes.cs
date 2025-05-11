using MicroOndas.Public;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Testes
{
    public class PublicTestes
    {
        /// <summary>
        /// Teste da classe excecao, se ela está funcionando corretamente.
        /// </summary>
        [Fact]
        public void TesteExcecao()
        {
            try
            {
                int dividendo = 5;
                int divisor = 0;

                dividendo = dividendo / divisor;
            }
            catch (Exception ex)
            {
                Excecao.GravaExcecao(ex);
            }
        }        
    }
}
