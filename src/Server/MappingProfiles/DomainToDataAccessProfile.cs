using AutoMapper;
using DataAccess.Entities;
using Server.Models.Domain;

namespace Server.MappingProfiles;

public class DomainToDataAccessProfile : Profile
{
    public DomainToDataAccessProfile()
    {
        CreateMap<User, ApplicationUser>();
    }
}
