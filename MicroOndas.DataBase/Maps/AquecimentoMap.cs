using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroOndas.DataBase.Models;

namespace MicroOndas.DataBase.Maps
{
    internal class AquecimentoMap
    {
        public AquecimentoMap(ref ModelBuilder modelBuilder)
        {
            Action<EntityTypeBuilder<AquecimentoModel>> map = config =>
            {
                config.Property(p => p.Index).HasColumnName("Index");
                config.Property(p => p.Inicio).HasColumnName("Inicio");
                config.Property(p => p.Fim).HasColumnName("Fim");
                config.Property(p => p.Pausa).HasColumnName("Pausa");
                config.Property(p => p.Potencia).HasColumnName("Potencia");
                config.Property(p => p.Ativo).HasColumnName("Ativo");
                config.Property(p => p.Cancelado).HasColumnName("Cancelado");

                config.ToTable("Aquecimento");
            };

            modelBuilder.Entity(map);
            modelBuilder.Entity<AquecimentoModel>().HasKey(k => k.Index);
        }
    }
}
