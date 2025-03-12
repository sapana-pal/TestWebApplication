using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestWebApplication.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DemoEntities : DbContext
    {
        public DemoEntities() : base("demoASPEntities") // Ensure this exists in Web.config
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<InsuranceQuoteRequest> InsuranceQuotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Disable database initialization (optional, uncomment if needed)
            // Database.SetInitializer<DemoEntities>(null);

            // Define table mappings
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<InsuranceQuoteRequest>().ToTable("InsuranceQuoteRequests");

            // Prevent EF from pluralizing table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }

}
