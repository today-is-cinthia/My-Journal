using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using My_Journal.Models.Divisa;
using My_Journal.Models.Ofrenda;
using My_Journal.Models.OfrendaCategoria;
using My_Journal.Models.Pagos;
using My_Journal.Models.PagosCategoria;
using My_Journal.Models.PagosDetalle;

namespace My_Journal;

public partial class CbnIglesiaContext : DbContext
{
    public CbnIglesiaContext()
    {
    }

    public CbnIglesiaContext(DbContextOptions<CbnIglesiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OfrendasCategoria> OfrendasCategorias { get; set; }
    public virtual DbSet<Diezmo> Diezmos { get; set; }

    public virtual DbSet<DiezmoDetalle> DiezmoDetalles { get; set; }

    public virtual DbSet<Divisa> Divisas { get; set; }

    public virtual DbSet<EgresosVario> EgresosVarios { get; set; }

    public virtual DbSet<EgresosVariosDetalle> EgresosVariosDetalles { get; set; }

    public virtual DbSet<Iglesium> Iglesia { get; set; }

    public virtual DbSet<IngresosVario> IngresosVarios { get; set; }

    public virtual DbSet<IngresosVariosDetalle> IngresosVariosDetalles { get; set; }

    public virtual DbSet<Miembro> Miembros { get; set; }

    public virtual DbSet<Ofrenda> Ofrendas { get; set; }

    public virtual DbSet<OfrendaPatoral> OfrendaPatorals { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<PagosCategoria> PagosCategorias { get; set; }

    public virtual DbSet<PagosDetalle> PagosDetalles { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost;database=CBN_IGLESIA;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfrendasCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCatOfrenda).HasName("PK__OfrendasCategoria__3BB07412B1FBF2D1");

            entity.ToTable("OfrendasCategoria", "ADM");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.OfrendasCategorias)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__Diezmo__UsuarioC__4222D4EF");
        });

        modelBuilder.Entity<Diezmo>(entity =>
        {
            entity.HasKey(e => e.IdDiezmo).HasName("PK__Diezmo__3BB07412B1FBF2D1");

            entity.ToTable("Diezmo", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaDiezmo).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.Diezmos)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__Diezmo__UsuarioC__4222D4EF");
        });

        modelBuilder.Entity<DiezmoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetDiezmo).HasName("PK__DiezmoDe__0E33FD862A2D99B2");

            entity.ToTable("DiezmoDetalle", "IGLESIA");

            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.DivisaNavigation).WithMany(p => p.DiezmoDetalles)
                .HasForeignKey(d => d.Divisa)
                .HasConstraintName("FK__DiezmoDet__Divis__45F365D3");

            entity.HasOne(d => d.IdDiezmoNavigation).WithMany(p => p.DiezmoDetalles)
                .HasForeignKey(d => d.IdDiezmo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiezmoDet__IdDie__46E78A0C");

            entity.HasOne(d => d.IdMiembroNavigation).WithMany(p => p.DiezmoDetalles)
                .HasForeignKey(d => d.IdMiembro)
                .HasConstraintName("FK__DiezmoDet__IdMie__47DBAE45");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.DiezmoDetalles)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__DiezmoDet__Usuar__44FF419A");
        });

        modelBuilder.Entity<Divisa>(entity =>
        {
            entity.HasKey(e => e.IdDivisa).HasName("PK__Divisas__DA960DCB2A730F13");

            entity.ToTable("Divisas", "ADM");

            entity.Property(e => e.CodDivisa).HasMaxLength(20);
            entity.Property(e => e.Descripcion).HasMaxLength(20);
            entity.Property(e => e.Simbolo).HasMaxLength(5);
        });

        modelBuilder.Entity<EgresosVario>(entity =>
        {
            entity.HasKey(e => e.IdEgreVarios).HasName("PK__EgresosV__2FC8D0E0771E5250");

            entity.ToTable("EgresosVarios", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.EgresosVarios)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__EgresosVa__IdPro__6EF57B66");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EgresosVarios)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__EgresosVa__Usuar__6FE99F9F");
        });

        modelBuilder.Entity<EgresosVariosDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__EgresosV__E43646A5D0D30B04");

            entity.ToTable("EgresosVariosDetalle", "IGLESIA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.DivisaNavigation).WithMany(p => p.EgresosVariosDetalles)
                .HasForeignKey(d => d.Divisa)
                .HasConstraintName("FK__EgresosVa__Divis__75A278F5");

            entity.HasOne(d => d.IdEgreVariosNavigation).WithMany(p => p.EgresosVariosDetalles)
                .HasForeignKey(d => d.IdEgreVarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EgresosVa__IdEgr__72C60C4A");

            entity.HasOne(d => d.IdMiembroNavigation).WithMany(p => p.EgresosVariosDetalles)
                .HasForeignKey(d => d.IdMiembro)
                .HasConstraintName("FK__EgresosVa__IdMie__74AE54BC");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EgresosVariosDetalles)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__EgresosVa__Usuar__73BA3083");
        });

        modelBuilder.Entity<Iglesium>(entity =>
        {
            entity.HasKey(e => e.IdIglesia).HasName("PK__Iglesia__5D353C1A1B8E5D40");

            entity.ToTable("Iglesia", "ADM");

            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Esquema).HasMaxLength(20);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<IngresosVario>(entity =>
        {
            entity.HasKey(e => e.IdIngreVarios).HasName("PK__Ingresos__0413C7C5F4E5D459");

            entity.ToTable("IngresosVarios", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.IngresosVarios)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK__IngresosV__IdPro__4CA06362");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.IngresosVarios)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__IngresosV__Usuar__4D94879B");
        });

        modelBuilder.Entity<IngresosVariosDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Ingresos__E43646A5F90E4641");

            entity.ToTable("IngresosVariosDetalle", "IGLESIA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.DivisaNavigation).WithMany(p => p.IngresosVariosDetalles)
                .HasForeignKey(d => d.Divisa)
                .HasConstraintName("FK__IngresosV__Divis__534D60F1");

            entity.HasOne(d => d.IdIngreVariosNavigation).WithMany(p => p.IngresosVariosDetalles)
                .HasForeignKey(d => d.IdIngreVarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IngresosV__IdIng__5070F446");

            entity.HasOne(d => d.IdMiembroNavigation).WithMany(p => p.IngresosVariosDetalles)
                .HasForeignKey(d => d.IdMiembro)
                .HasConstraintName("FK__IngresosV__IdMie__52593CB8");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.IngresosVariosDetalles)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__IngresosV__Usuar__5165187F");
        });

        modelBuilder.Entity<Miembro>(entity =>
        {
            entity.HasKey(e => e.IdMiembro).HasName("PK__Miembros__7B9226C8D765AD4D");

            entity.ToTable("Miembros", "IGLESIA");

            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.FechaBautismo).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.Miembros)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__Miembros__Usuari__3F466844");
        });

        modelBuilder.Entity<Ofrenda>(entity =>
        {
            entity.HasKey(e => e.IdOfrenda).HasName("PK__Ofrendas__252FE8A595EF4A3E");

            entity.ToTable("Ofrendas", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.Ofrenda)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__Ofrendas__Usuari__59063A47");
        });

        modelBuilder.Entity<OfrendaPatoral>(entity =>
        {
            entity.HasKey(e => e.IdOfrePasto).HasName("PK__OfrendaP__7FDB1189941D10B5");

            entity.ToTable("OfrendaPatoral", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        //modelBuilder.Entity<OfrendasCategoria>(entity =>
        //{
        //    entity.HasKey(e => e.IdCatOfrenda).HasName("PK__Ofrendas__A053EF0856DEF2C8");

        //    entity.ToTable("OfrendasCategoria", "IGLESIA");

        //    entity.Property(e => e.Descripcion).HasMaxLength(200);
        //    entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
        //    entity.Property(e => e.FechaModifica).HasColumnType("datetime");
        //    entity.Property(e => e.Nombre).HasMaxLength(50);

        //    entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.OfrendasCategoria)
        //        .HasForeignKey(d => d.UsuarioCreacion)
        //        .HasConstraintName("FK__OfrendasC__Usuar__5629CD9C");
        //});

       
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos__FC851A3A7B6919AF");

            entity.ToTable("Pagos", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__Pagos__UsuarioCr__66603565");
        });

        modelBuilder.Entity<PagosCategoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__PagosCat__A3C02A101A9211F0");

            entity.ToTable("PagosCategorias", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PagosCategoria)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__PagosCate__Usuar__6383C8BA");
        });

        modelBuilder.Entity<PagosDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__PagosDet__E43646A579B9C150");

            entity.ToTable("PagosDetalle", "IGLESIA");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");

            entity.HasOne(d => d.DivisaNavigation).WithMany(p => p.PagosDetalles)
                .HasForeignKey(d => d.Divisa)
                .HasConstraintName("FK__PagosDeta__Divis__6B24EA82");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.PagosDetalles)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PagosDeta__IdCat__6A30C649");

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.PagosDetalles)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PagosDeta__IdPag__693CA210");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PagosDetalles)
                .HasForeignKey(d => d.UsuarioCreacion)
                .HasConstraintName("FK__PagosDeta__Usuar__6C190EBB");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.IdProyecto).HasName("PK__Proyecto__F4888673CE4C17E6");

            entity.ToTable("Proyecto", "IGLESIA");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584C6251E999");

            entity.ToTable("Roles", "ADM");

            entity.Property(e => e.CodRol).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__5B65BF97068BE899");

            entity.ToTable("Usuarios", "ADM");

            entity.Property(e => e.Apellidos).HasMaxLength(50);
            entity.Property(e => e.Clave).HasMaxLength(20);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModifica).HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(20)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
