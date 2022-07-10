using AutoMapper;
using DataAccess.Entities;
using Server.Models.Domain;

namespace Server.MappingProfiles;

public class DataAccessToDomainProfile : Profile
{
    public DataAccessToDomainProfile()
    {
        CreateMap<ApplicationUser, User>();
    }
}
