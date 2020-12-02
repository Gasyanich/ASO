using ASO.Models.DTO;
using ASO.Models.Responses;
using AutoMapper;

namespace ASO.Models.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, RoleResponse>();
        }
    }
}