using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleLeasingApp.Models;

namespace VehicleLeasingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Drivers> Drivers { get; set; }

    }
}
