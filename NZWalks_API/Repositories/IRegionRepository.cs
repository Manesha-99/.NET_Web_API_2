using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync();

       Task<Region?> GetByIDAsync(Guid id);

       Task<Region> CreateAsync(Region region);

       Task<Region?> ChangeByIDAsync(Guid id, Region region);

       Task<Region?> DeleteAsync(Guid id);

       
      
    }
}
