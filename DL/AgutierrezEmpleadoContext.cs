using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class AgutierrezEmpleadoContext : DbContext
{
    public AgutierrezEmpleadoContext()
    {
    }

    public AgutierrezEmpleadoContext(DbContextOptions<AgutierrezEmpleadoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatEntidadFederativa> CatEntidadFederativas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= AGutierrezEmpleado; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatEntidadFederativa>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__CatEntid__FBB0EDC1D460E8CE");

            entity.ToTable("CatEntidadFederativa");

            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E4DC0FBAD");

            entity.ToTable("Empleado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroNomina)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Empleado__IdEsta__1A14E395");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
