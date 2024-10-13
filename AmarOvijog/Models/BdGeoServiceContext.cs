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
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; } = null!;


        public virtual DbSet<Complaint> Complaints { get; set; } = null!;
        public virtual DbSet<ComplaintImage> ComplaintImages { get; set; } = null!;


    }
}
