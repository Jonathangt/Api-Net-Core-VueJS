using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Config
{
    class LibroConfig : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> entity)
        {
            entity.HasKey(e => e.IdLibro);

            entity.ToTable("libro");

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");

            entity.Property(e => e.AutorApellido)
                .HasColumnName("autor_apellido")
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.AutorNombre)
                .HasColumnName("autor_nombre")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Categoria)
                .HasColumnName("categoria")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Precio)
                .HasColumnName("precio")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
        
    }
}
