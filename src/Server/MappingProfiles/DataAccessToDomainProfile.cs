using AutoMapper;
using DataAccess.Entities;
using Server.Models.Domain;
using Server.Models.RequestDtos;

namespace Server.MappingProfiles;

public class DataAccessToDomainProfile : Profile
{
    public DataAccessToDomainProfile()
    {
        CreateMap<ApplicationUser, User>();
    }
}
