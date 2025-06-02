using MicroOndas.DataBase.Maps;
using MicroOndas.DataBase.Models;
using MicroOndas.Public;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase
{
    public class Context : DbContext
    {
        public Context()
        {
            if (this.Database.EnsureCreated())
                this.Database.Migrate();
        }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<AquecimentoModel> Aquecimento { get; set; }
        public DbSet<ProgramadoModel> Programado { get; set; }
        public DbSet<VariaveisModel> Variaveis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string key = "5fzYQ9OYxPQKQxFCFMdq9gaaqfjMd7SGMreIZEX3xaHgAfO2+nhexGcZLu4loffVhCf4I6S9RZwFGIo6nepxOufJZNNSP+Lfzz87AdRCe6y5W7QDq1K9A29j1+3kdRTC6osSkOjpE1flk/LGGZgMEM4lmFDm2nNV2vY5w6w0zeETIMPBlFywh2czdxA9YefCXSxK38gfLeT0ekbt9Fo2muUfdB/Oo0Oc89XQjHemaf0=";
            string conn = CryptoHelper.Decrypt(key);
            optionsBuilder.UseSqlServer(conn);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new LoginMap(ref modelBuilder);
            new AquecimentoMap(ref modelBuilder);
            new ProgramadoMap(ref modelBuilder);
            new VariaveisMap(ref modelBuilder);
        }
    }
}
