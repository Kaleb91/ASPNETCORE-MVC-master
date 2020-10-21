using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cooperchip.ITDeveloper.Data.Mapping
{
    public class MedicamentoMap : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(p => p.Codigo).IsRequired();
            builder.Property(p => p.CodigoGenerico).IsRequired();
            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(4000).HasColumnName("Descricao").HasColumnType("nvarchar(4000)");
            builder.Property(p => p.Generico).HasMaxLength(4000).HasColumnName("Generico").HasColumnType("nvarchar(4000)").IsRequired();

            builder.ToTable("Medicamento");
        }
    }
}
