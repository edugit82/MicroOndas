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
    internal class VariaveisMap
    {
        public VariaveisMap(ref ModelBuilder modelBuilder)
        {
            Action<EntityTypeBuilder<VariaveisModel>> map = config =>
            {
                config.Property(p => p.Index).HasColumnName("Index");
                config.Property(p => p.Descricao).HasColumnName("Descricao");
                config.Property(p => p.Valor).HasColumnName("Valor");

                config.ToTable("Variaveis");
            };

            modelBuilder.Entity(map);
            modelBuilder.Entity<VariaveisModel>().HasKey(k => k.Index);
        }
    }
}
