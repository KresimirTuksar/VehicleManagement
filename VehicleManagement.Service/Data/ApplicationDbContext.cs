using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Service.Models.Entities;

namespace VehicleManagement.Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=VehicleManagementDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>()
                .HasOne(v => v.Make)
                .WithMany(m => m.Models)
                .HasForeignKey(v => v.MakeId);

            // Add any other model configurations here
        }
    }
}
