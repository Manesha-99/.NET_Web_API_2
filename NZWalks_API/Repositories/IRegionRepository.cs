using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync(string? filterOn=null, string? filterQuery =null, string? sortBy=null, 
           bool isAscending=true, int pageNumber=1, int pageSize=10);

       Task<Region?> GetByIDAsync(Guid id);

       Task<Region> CreateAsync(Region region);

       Task<Region?> ChangeByIDAsync(Guid id, Region region);

       Task<Region?> DeleteAsync(Guid id);

       
      
    }
}
