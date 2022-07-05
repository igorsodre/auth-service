using AutoMapper;
using Server.Models.Domain;
using Server.Models.ResponseDtos;

namespace Server.MappingProfiles;

public class DomainToExternalProfile : Profile
{
    public DomainToExternalProfile()
    {
        CreateMap<LoginResult, LoginResponse>();
    }
}
