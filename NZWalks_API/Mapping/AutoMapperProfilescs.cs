using AutoMapper;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Mapping
{
    public class AutoMapperProfilescs: Profile
    {
        public AutoMapperProfilescs()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<ChangeRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<ChangeWalkDto, Walk>().ReverseMap();
        }
    }
}
