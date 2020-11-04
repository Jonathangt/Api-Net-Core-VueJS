using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Config
{
    class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");

            entity.Property(e => e.Apellido)
                .HasColumnName("apellido")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Ciudad)
                .HasColumnName("ciudad")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CodigoPostal)
                .HasColumnName("codigo_postal")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Direccion)
                .HasColumnName("direccion")
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.Estado)
                .HasColumnName("estado")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Nombre)
                .HasColumnName("nombre")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Telefono)
                .HasColumnName("telefono")
                .HasMaxLength(15)
                .IsUnicode(false);
        }
    }
}
