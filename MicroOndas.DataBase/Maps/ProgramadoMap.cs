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
    internal class ProgramadoMap
    {
        public ProgramadoMap(ref ModelBuilder modelBuilder)
        {
            Action<EntityTypeBuilder<ProgramadoModel>> map = config =>
            {
                config.Property(p => p.Index).HasColumnName("Index");
                config.Property(p => p.Nome).HasColumnName("Nome");
                config.Property(p => p.Alimento).HasColumnName("Alimento");
                config.Property(p => p.Tempo).HasColumnName("Tempo");
                config.Property(p => p.Potencia).HasColumnName("Potencia");
                config.Property(p => p.Caracter).HasColumnName("Caracter");
                config.Property(p => p.Instrucoes).HasColumnName("Instrucoes");

                config.ToTable("Programado");
            };

            modelBuilder.Entity(map);
            modelBuilder.Entity<ProgramadoModel>().HasKey(k => k.Index);
        }
    }
}
