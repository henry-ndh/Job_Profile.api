using AutoMapper;
using Udemy.DotNetWebAPI.Models.Domain;
using Udemy.DotNetWebAPI.Models.DTO;

namespace Udemy.DotNetWebAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
