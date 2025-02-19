using Microsoft.AspNetCore.Mvc.Infrastructure;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateById(Guid id, Walk walk);

        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
