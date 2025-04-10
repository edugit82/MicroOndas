using MicroOndas.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.DataBase.Maps
{
    internal class AquecimentoMap
    {
        public AquecimentoMap(ref DbModelBuilder modelBuilder)
        {
            Action<EntityMappingConfiguration<AquecimentoModel>> map = config =>
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

            modelBuilder.Entity<AquecimentoModel>().Map(map);
            modelBuilder.Entity<AquecimentoModel>().HasKey(k => k.Index);
        }
    }
}
