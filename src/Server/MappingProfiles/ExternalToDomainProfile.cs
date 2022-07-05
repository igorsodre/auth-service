using AutoMapper;
using Server.Models.Domain;
using Server.Models.RequestDtos;

namespace Server.MappingProfiles;

public class ExternalToDomainProfile : Profile
{
    public ExternalToDomainProfile()
    {
        CreateMap<ExternalAuthRequest, ExternalAuthPayload>();
    }
}
