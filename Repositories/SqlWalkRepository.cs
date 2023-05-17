using first_web_api.Models.Domain;
using first_web_api.Data;
using Microsoft.EntityFrameworkCore;

namespace first_web_api.Repositories
{
    public class SqlWalkRepository : IWalkReposotory
    {
        private readonly FirstWebApiDBContext dbContext;

        public SqlWalkRepository(FirstWebApiDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walkEntity = await dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);
            if (walkEntity == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walkEntity);
            await dbContext.SaveChangesAsync();


            return walkEntity;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            List<Walk> walks = await dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .ToListAsync();

            return walks;
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            Walk? walk = await dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .FirstOrDefaultAsync(walk => walk.Id == id);

            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk updateWalk)
        {
            Walk? entityWalk = await dbContext.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (entityWalk == null)
            {
                return null;
            }

            entityWalk.Name = updateWalk.Name;
            entityWalk.Description = updateWalk.Description;
            entityWalk.LenghtInKm = updateWalk.LenghtInKm;
            entityWalk.WalkImageUrl = updateWalk.WalkImageUrl;
            entityWalk.DifficultyId = updateWalk.DifficultyId;
            entityWalk.RegionId = updateWalk.RegionId;

            await dbContext.SaveChangesAsync();

            return entityWalk;
        }
    }
}