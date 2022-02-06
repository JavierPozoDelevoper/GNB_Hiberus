using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GNBCoreWebAPI.Entities
{
    public partial class GNBContext : DbContext
    {
        public GNBContext()
        {
        }

        public GNBContext(DbContextOptions<GNBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Rate> Rates { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=Localhost;Database=GNB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Idcurrency);

                entity.ToTable("Currency");

                entity.Property(e => e.Idcurrency)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDCurrency");

                entity.Property(e => e.CodIso)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CodISO");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(e => e.Idrate);

                entity.ToTable("Rate");

                entity.Property(e => e.Idrate)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDRate");

                entity.Property(e => e.IdcurrencyFrom)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDCurrencyFrom");

                entity.Property(e => e.IdcurrencyTo)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDCurrencyTo");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.IdcurrencyFromNavigation)
                    .WithMany(p => p.RateIdcurrencyFromNavigations)
                    .HasForeignKey(d => d.IdcurrencyFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rate_CurrencyFrom");

                entity.HasOne(d => d.IdcurrencyToNavigation)
                    .WithMany(p => p.RateIdcurrencyToNavigations)
                    .HasForeignKey(d => d.IdcurrencyTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rate_CurrencyTo");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Idtransaction);

                entity.ToTable("Transaction");

                entity.Property(e => e.Idtransaction)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDTransaction");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Idcurrency)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("IDCurrency");

                entity.Property(e => e.Sku)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdcurrencyNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Idcurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Currency");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
