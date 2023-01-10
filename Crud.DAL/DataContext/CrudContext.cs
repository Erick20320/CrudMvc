using System;
using System.Collections.Generic;
using Crud.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.DAL.DataContext;

public partial class CrudContext : IdentityDbContext<Usuario>
{
    public CrudContext()
    {
    }

    public CrudContext(DbContextOptions<CrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Producto>();

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3F953AB0C");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
