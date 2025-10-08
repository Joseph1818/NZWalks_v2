using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories;

    // Ensure SQLWalkRepository implements IWalkRepository
    public class SQLWalkRepository : IWalkRepository
    {
        public readonly NZWalksDbContext dbcontext;

        public SQLWalkRepository(NZWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.walk.AddAsync(walk);
            await dbcontext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
             // Include() function is a navigation property to retrieve also information of Region and Difficluty
            return await dbcontext.walk.Include("Region").Include("Difficulty").ToListAsync();      
        }
        
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbcontext.walk.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
        } 
        
        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbcontext.walk.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            await dbcontext.SaveChangesAsync();
            return existingWalk;
        }   
        
        public async Task<Walk?> DeleteAsync(Guid id)
        {
        var existingWalk = await dbcontext.walk.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }
            dbcontext.walk.Remove(existingWalk);
            await dbcontext.SaveChangesAsync();
            return existingWalk;
         }

        

    }

