using Microsoft.EntityFrameworkCore;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region?> ChangeByIDAsync(Guid id, Region region)
        {
            var existRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existRegion == null)
            {
                return null;
            }

            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existRegion;

        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existregion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existregion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existregion);
            await dbContext.SaveChangesAsync();
            return existregion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return  await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIDAsync(Guid id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
