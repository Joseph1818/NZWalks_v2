using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRequestRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRequestRegionDto, Region>().ReverseMap();

            // Mapping for other domain models and DTOs
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddRequestWalkDto, Walk>().ReverseMap();
            CreateMap<UpdateRequestWalkDto, Walk>().ReverseMap();

        }
    }
}
