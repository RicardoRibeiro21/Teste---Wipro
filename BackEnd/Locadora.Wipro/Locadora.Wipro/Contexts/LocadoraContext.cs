using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Locadora.Wipro.Domains;

namespace Locadora.Wipro.Contexts
{
    public partial class LocadoraContext : DbContext
    {
        public LocadoraContext()
        {
        }

        public LocadoraContext(DbContextOptions<LocadoraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> TbCliente { get; set; }
        public virtual DbSet<Filme> TbFilme { get; set; }
        public virtual DbSet<Locacao> TbLocacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ROGER\\SQLEXPRESS01;Initial Catalog=db_locadora;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__tb_Clien__885457EED9FB718D");

                entity.ToTable("tb_Cliente");

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__tb_Clien__C1F89731D6B704B0")
                    .IsUnique();

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DtNascimento)
                    .HasColumnName("dtNascimento")
                    .HasColumnType("date");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasColumnName("nomeCliente")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Filme>(entity =>
            {
                entity.HasKey(e => e.IdFilme)
                    .HasName("PK__tb_Filme__96709919538622C4");

                entity.ToTable("tb_Filme");

                entity.Property(e => e.IdFilme).HasColumnName("idFilme");

                entity.Property(e => e.Disponibilidade).HasColumnName("disponibilidade");

                entity.Property(e => e.DtLancamento)
                    .HasColumnName("dtLancamento")
                    .HasColumnType("date");

                entity.Property(e => e.NomeFilme)
                    .IsRequired()
                    .HasColumnName("nomeFilme")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locacao>(entity =>
            {
                entity.HasKey(e => e.IdLocacao)
                    .HasName("PK__tb_Locac__906545EC94C3D836");

                entity.ToTable("tb_Locacao");

                entity.Property(e => e.IdLocacao).HasColumnName("idLocacao");

                entity.Property(e => e.DtEntrega)
                    .HasColumnName("dtEntrega")
                    .HasColumnType("date");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdFilme).HasColumnName("idFilme");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.TbLocacao)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Locaca__idCli__5812160E");

                entity.HasOne(d => d.IdFilmeNavigation)
                    .WithMany(p => p.TbLocacao)
                    .HasForeignKey(d => d.IdFilme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tb_Locaca__idFil__571DF1D5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
