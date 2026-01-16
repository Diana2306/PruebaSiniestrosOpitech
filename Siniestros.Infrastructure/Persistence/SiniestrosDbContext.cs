    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Persistence;
using Siniestros.Domain.Entities;

namespace Siniestros.Infrastructure.Persistence
{
    /// <summary>
    /// Contexto de base de datos de Entity Framework Core para el sistema de siniestros viales.
    /// Esta clase configura las entidades del dominio y sus relaciones, mapeándolas a las tablas
    /// de la base de datos SQL Server. Utiliza códigos DIVIPOLA como identificadores para
    /// departamentos (CHAR(2)) y ciudades (CHAR(5)).
    /// </summary>
    public class SiniestrosDbContext : DbContext, ISiniestrosDbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia del contexto con las opciones de configuración.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto, incluyendo la cadena de conexión.</param>
        public SiniestrosDbContext(DbContextOptions<SiniestrosDbContext> options) : base(options) { }

        /// <summary>
        /// DbSet para acceder a los siniestros viales en la base de datos.
        /// </summary>
        public DbSet<Siniestro> Siniestros => Set<Siniestro>();

        /// <summary>
        /// DbSet para acceder a las ciudades en la base de datos.
        /// </summary>
        public DbSet<Ciudad> Ciudades => Set<Ciudad>();

        /// <summary>
        /// DbSet para acceder a los departamentos en la base de datos.
        /// </summary>
        public DbSet<Departamento> Departamentos => Set<Departamento>();

        /// <summary>
        /// DbSet para acceder a los tipos de siniestros en la base de datos.
        /// </summary>
        public DbSet<TipoSiniestro> TiposSiniestro => Set<TipoSiniestro>();

        /// <summary>
        /// Configura el modelo de datos y las relaciones entre entidades.
        /// Define las propiedades de las columnas, índices, claves foráneas y restricciones
        /// necesarias para mapear correctamente las entidades del dominio a las tablas de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de Entity Framework Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Departamento>(e =>
            {
                e.ToTable("Departamento");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id)
                    .HasMaxLength(2)
                    .IsFixedLength()
                    .IsRequired();
                e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
                e.HasIndex(x => x.Nombre).IsUnique();
            });

            modelBuilder.Entity<Ciudad>(e =>
            {
                e.ToTable("Ciudad");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .IsRequired();
                e.Property(x => x.IdDepartamento)
                    .HasMaxLength(2)
                    .IsFixedLength()
                    .IsRequired();
                e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();

                e.HasOne(x => x.Departamento)
                    .WithMany(d => d.Ciudades)
                    .HasForeignKey(x => x.IdDepartamento)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.IdDepartamento, x.Nombre }).IsUnique();
            });

            modelBuilder.Entity<TipoSiniestro>(e =>
            {
                e.ToTable("TipoSiniestro");
                e.HasKey(x => x.Id);
                e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
                e.HasIndex(x => x.Nombre).IsUnique();
            });

            modelBuilder.Entity<Siniestro>(e =>
            {
                e.ToTable("Siniestros");
                e.HasKey(x => x.Id);

                e.Property(x => x.IdCiudad)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .IsRequired();

                e.HasOne(x => x.Ciudad)
                    .WithMany(c => c.Siniestros)
                    .HasForeignKey(x => x.IdCiudad)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.TipoSiniestro)
                    .WithMany(t => t.Siniestros)
                    .HasForeignKey(x => x.IdTipoSiniestro)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => x.FechaHora);
                e.HasIndex(x => x.IdCiudad);
                e.HasIndex(x => x.IdTipoSiniestro);
            });
        }
    }
}