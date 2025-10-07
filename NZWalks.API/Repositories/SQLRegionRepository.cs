using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        public readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.regions.ToListAsync();
        }
        
        public async Task<Region?> GetRegionByI(Guid id)
        {
            return await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region> AddAsync(Region region)
        {
            await dbContext.regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            
            
           await dbContext.SaveChangesAsync();

            return existingRegion;  
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
