using MicroOndas.DataBase.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase.Maps
{
    internal class LoginMap
    {
        public LoginMap(ref ModelBuilder modelBuilder)
        {
            Action<EntityTypeBuilder<LoginModel>> map = config =>
            {
                config.Property(p => p.Index).HasColumnName("Index");
                config.Property(p => p.Usuario).HasColumnName("Usuario");
                config.Property(p => p.Senha).HasColumnName("Senha");
                config.Property(p => p.Token).HasColumnName("Token");
                config.Property(p => p.TokenTime).HasColumnName("TokenTime");

                config.ToTable("Login");
            };

            modelBuilder.Entity(map);
            modelBuilder.Entity<LoginModel>().HasKey(k => k.Index);
        }
    }
}
