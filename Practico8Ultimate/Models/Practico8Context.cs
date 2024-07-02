using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practico8Ultimate.Models;

public partial class Practico8Context : DbContext
{
    public Practico8Context()
    {
    }

    public Practico8Context(DbContextOptions<Practico8Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Alquilere> Alquileres { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Copia> Copias { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=JUAN25;Initial Catalog=Practico8;Integrated Security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alquilere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alquiler__3214EC077C563061");

            entity.Property(e => e.FechaAlquiler).HasColumnType("datetime");
            entity.Property(e => e.FechaEntregada).HasColumnType("datetime");
            entity.Property(e => e.FechaTope).HasColumnType("datetime");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Alquileres_Clientes");

            entity.HasOne(d => d.IdCopiaNavigation).WithMany(p => p.Alquileres)
                .HasForeignKey(d => d.IdCopia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Alquileres_Copias");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC07A994E150");

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(256);
            entity.Property(e => e.DocumentoIdentidad).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Copia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Copias__3214EC073B8275DB");

            entity.Property(e => e.Formato).HasMaxLength(15);

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Copia)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Copias_Peliculas");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pelicula__3214EC074A1D50F2");

            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
