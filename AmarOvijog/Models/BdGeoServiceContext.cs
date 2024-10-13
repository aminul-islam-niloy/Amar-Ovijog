using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace AmarOvijog.Models
{
    public partial class BdGeoServiceContext : IdentityDbContext<ApplicationUser>
    {
        public BdGeoServiceContext()
        {
        }

        public BdGeoServiceContext(DbContextOptions<BdGeoServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Division> Divisions { get; set; } = null!;
        public virtual DbSet<Union> Unions { get; set; } = null!;
        public virtual DbSet<Upazila> Upazilas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=AMINUL\\SQLEXPRESS;Database=BdGeoService;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.BnName).HasMaxLength(25);

                entity.Property(e => e.Lat).HasMaxLength(15);

                entity.Property(e => e.Lon).HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(25);

                entity.Property(e => e.Url).HasMaxLength(50);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.DivisionId)
                    .HasConstraintName("FK_Districts_Divisions");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.Property(e => e.BnName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Bn_Name");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Union>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BnName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Bn_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpazillaId).HasColumnName("Upazilla_id");

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Upazilla)
                    .WithMany(p => p.Unions)
                    .HasForeignKey(d => d.UpazillaId)
                    .HasConstraintName("FK__Unions__Upazilla__3F466844");
            });

            modelBuilder.Entity<Upazila>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BnName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Bn_name");

                entity.Property(e => e.DistrictId).HasColumnName("District_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Upazilas)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__Upazilas__Distri__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
