using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{

    // This class inarite from the DbContext class from the Entity Framework Core
    public class NZWalksDbContext : DbContext
    {
        //Constructor       
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //Creating collection of each one of the Domain class
        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }  
        public DbSet<Walk> walk { get; set; }   
    }
}
