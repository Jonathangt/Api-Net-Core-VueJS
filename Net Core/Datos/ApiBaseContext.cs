using System;
using Datos.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;

namespace Datos
{
    public partial class ApiBaseContext : DbContext
    {
        public ApiBaseContext()
        {
        }

        public ApiBaseContext(DbContextOptions<ApiBaseContext> options)
            : base(options)
        {
        }

        //Los dbSet son colecciones
        public DbSet<Cliente> dbCliente { get; set; }
        public DbSet<Libro> dbLibro { get; set; }
        public DbSet<Orden> dbOrden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ApiCore;Trusted_Connection=True;");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se implementa las clases de configuracion de cada entidad
            //que se encuentran en config
            modelBuilder.ApplyConfiguration(new ClienteConfig());
            modelBuilder.ApplyConfiguration(new LibroConfig());
            modelBuilder.ApplyConfiguration(new OrdenConfig());

         
        }
    }
}
