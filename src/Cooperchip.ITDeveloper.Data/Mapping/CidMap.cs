using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cooperchip.ITDeveloper.Data.Mapping
{
    public class CidMap : IEntityTypeConfiguration<Cid>
    {
        public void Configure(EntityTypeBuilder<Cid> builder)
        {
            builder.ToTable("Cid");
            builder.HasKey(n => n.Id);

            builder.Property(p => p.Codigo).HasColumnName("Codigo").HasColumnType("varchar(6)");
            builder.Property(p => p.CidInternalId).HasColumnName("CidInternalId");
            builder.Property(p => p.Diagnostico).HasColumnName("Diagnostico").HasColumnType("nvarchar(4000)");
        }
    }
}
