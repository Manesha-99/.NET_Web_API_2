using Microsoft.AspNetCore.Mvc.Diagnostics;
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

        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber=1, int pageSize=10)
        {

           //Filtering

            var regions = dbContext.Regions.AsQueryable();

            if((string.IsNullOrWhiteSpace(filterOn) ==false) && (string.IsNullOrWhiteSpace(filterQuery) ==false))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = regions.Where(x=>x.Name.Contains(filterQuery));
                }
            }

            //Sorting

            if((string.IsNullOrWhiteSpace(sortBy) == false))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = isAscending ? regions.OrderBy(x=> x.Name) : regions.OrderByDescending(x=> x.Name);
                }
                else if(sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase)){

                    regions = isAscending ? regions.OrderBy(x=> x.Code) : regions.OrderByDescending(x=> x.Code);
                }
            }


            //Pagination

            var skipresults = (pageNumber - 1) * pageSize;

           return  await regions.Skip(skipresults).Take(pageSize).ToListAsync();
        }

        public async Task<Region?> GetByIDAsync(Guid id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
