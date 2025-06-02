using MicroOndas.DataBase;
using MicroOndas.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class GetProgramas
    {
        public List<ProgramadoModel> Retorno = new List<ProgramadoModel>();
        public GetProgramas() 
        {
            using (Context ctx = new Context()) 
            {
                this.Retorno = ctx.Programado.ToList().OrderBy(a => a.Index).ToList();
            }
        }
    }
}
