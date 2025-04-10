using MicroOndas.DataBase.Maps;
using MicroOndas.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase
{
    public class Context : DbContext
    {
        public Context(string conn) : base(conn)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());
        }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<AquecimentoModel> Aquecimento { get; set; }
        public DbSet<ProgramadoModel> Programado { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new LoginMap(ref modelBuilder);
            new AquecimentoMap(ref modelBuilder);
            new ProgramadoMap(ref modelBuilder);
        }
    }
}
