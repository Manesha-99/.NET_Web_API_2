using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return(walk);
        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
            var existWalkModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existWalkModel != null) { 
                
                dbContext.Walks.Remove(existWalkModel);
                await dbContext.SaveChangesAsync();
                return existWalkModel;
            }

            return null;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var existModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existModel == null)
            {
                return null;
            }

            return existModel;
        }

        public async Task<Walk?> UpdateById(Guid id, Walk walk)
        {
            var existWalkModel = await dbContext.Walks.FirstOrDefaultAsync(x=> x.Id == id);
            if(existWalkModel == null)
            {
                return null;
            }

            existWalkModel.Name = walk.Name;
            existWalkModel.Description = walk.Description;
            existWalkModel.LengthInKm = walk.LengthInKm;
            existWalkModel.WalkImageUrl = walk.WalkImageUrl;
            existWalkModel.DifficultyId = walk.DifficultyId;
            existWalkModel.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existWalkModel;
        }
    }
}
