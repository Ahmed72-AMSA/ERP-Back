using AutoMapper;
using erp_back.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Authentication, LoginDto>();
        CreateMap<LoginDto, Authentication>();
    }
}
