using AutoMapper;
using first_web_api.Models.Domain;
using first_web_api.Models.DTO;

namespace first_web_api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();

            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<UpdateWalkRequestDto, Walk>();

            CreateMap<Difficulty, DifficultyDto>();
        }
    }
}