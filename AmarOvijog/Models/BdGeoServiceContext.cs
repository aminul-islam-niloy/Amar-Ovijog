using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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

        public virtual DbSet<Complaint> Complaints { get; set; } = null!;
        public virtual DbSet<ComplaintImage> ComplaintImages { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });



            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasOne(c => c.User)
                      .WithMany() // Assuming one-to-many from ApplicationUser to Complaint
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.Division)
                      .WithMany() // Assuming Division can have many complaints
                      .HasForeignKey(c => c.DivisionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.District)
                      .WithMany() // Assuming District can have many complaints
                      .HasForeignKey(c => c.DistrictId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Upazila)
                      .WithMany() // Assuming Upazila can have many complaints
                      .HasForeignKey(c => c.UpazilaId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Union)
                      .WithMany() // Assuming Union can have many complaints
                      .HasForeignKey(c => c.UnionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ComplaintImage>(entity =>
            {
                entity.HasOne(ci => ci.Complaint)
                      .WithMany(c => c.ComplaintImages)
                      .HasForeignKey(ci => ci.ComplaintId)
                      .OnDelete(DeleteBehavior.Cascade);
            });




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
