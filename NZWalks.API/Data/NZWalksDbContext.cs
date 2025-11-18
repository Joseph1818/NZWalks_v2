using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;


namespace NZWalks.API.Data
{

    // This class inarite from the DbContext class from the Entity Framework Core
    public class NZWalksDbContext : DbContext
    {
        //Constructor       
        public NZWalksDbContext(DbContextOptions <NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //Creating collection of each one of the Domain class
        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> walk { get; set; }
        public DbSet<Image> images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>()
            {
            new Difficulty()
                {
                    Id = Guid.Parse("6b4f8c2e-1d4d-4f3e-8b9d-1c2e3f4a5b6c"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("7c5f9d3f-2e5e-4f4f-9c0e-2d3f4f5a6b7c"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8d6e0e4d-3f6f-4f5e-0d1f-3e4f5a6b7c8d"),
                    Name = "Hard"
                }
            };

            // Seed difficluties database using Entity Framework Core
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for regions

            var regions = new List<Region>()
            {
                 new Region()
                {
                    Id = Guid.Parse("1a2b3c4d-5e6f-4a8b-9c0d-1e2f3a4b5c6d"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://example.com/auckland.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("2b3c4d5e-6f7a-4b9c-0d1e-2f3a4b5c6d7e"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl = "https://example.com/wellington.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("3c4d5e6f-7a8b-4c0d-1e2f-3a4b5c6d7e8f"),
                    Name = "Christchurch",
                    Code = "CHC",
                    RegionImageUrl = "https://example.com/christchurch.jpg"
                }
                
            };

            //seed regions to the database
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
