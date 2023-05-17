using first_web_api.Models.Domain;
using first_web_api.Data;

using Microsoft.EntityFrameworkCore;

namespace first_web_api.Repositories
{
    public class SqlRegionRepository : IRegionReposotory
    {
        private readonly FirstWebApiDBContext dbContext;

        public SqlRegionRepository(FirstWebApiDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync<Region>();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync<Region>(region => region.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            Region? regionEntity = await dbContext.Regions.FirstOrDefaultAsync<Region>(region => region.Id == id);
            if (regionEntity == null)
            {
                return null;
            }

            regionEntity.Name = region.Name;
            regionEntity.Code = region.Code;
            regionEntity.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();


            return regionEntity;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region? regionEntity = await dbContext.Regions.FirstOrDefaultAsync<Region>(region => region.Id == id);
            if (regionEntity == null)
            {
                return null;
            }

            dbContext.Regions.Remove(regionEntity);
            await dbContext.SaveChangesAsync();


            return regionEntity;
        }
    }

}