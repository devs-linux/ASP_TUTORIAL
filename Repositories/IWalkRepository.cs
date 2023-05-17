using first_web_api.Models.Domain;

namespace first_web_api.Repositories
{
    public interface IWalkReposotory
    {
        Task<List<Walk>> GetAllAsync();

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk> CreateAsync(Walk walk);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }

}