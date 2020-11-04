using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Config
{
    class OrdenConfig : IEntityTypeConfiguration<Orden>
    {
       //public string NombreCliente { get; set; }
       // public string TituloLibro { get; set; }

        public void Configure(EntityTypeBuilder<Orden> entity)
        {
            entity.HasKey(e => e.IdOrden);

            entity.ToTable("orden");

            entity.Property(e => e.IdOrden).HasColumnName("idOrden");

           

            entity.Property(e => e.FechaOrden)
                .HasColumnName("fecha_orden")
                .HasMaxLength(50)
                .IsUnicode(false);

          

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");           

           

            entity.HasOne(d => d.Cliente)
                .WithMany(p => p.Orden)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("Fk_orden_libro");

            entity.HasOne(d => d.Libro)
                .WithMany(p => p.Orden)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("Fk_orden_cliente");
        }

        
    }
}
