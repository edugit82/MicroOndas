using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOndas.DataBase.Models;

namespace MicroOndas.DataBase.Maps
{
    internal class LoginMap
    {
        public LoginMap(ref DbModelBuilder modelBuilder)
        {
            Action<EntityMappingConfiguration<LoginModel>> map = config =>
            {
                config.Property(p => p.Index).HasColumnName("Index");
                config.Property(p => p.Usuario).HasColumnName("Usuario");
                config.Property(p => p.Senha).HasColumnName("Senha");

                config.ToTable("Login");
            };

            modelBuilder.Entity<LoginModel>().Map(map);
            modelBuilder.Entity<LoginModel>().HasKey(k => k.Index);
        }
    }
}
