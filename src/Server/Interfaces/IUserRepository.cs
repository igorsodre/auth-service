using DataAccess.Entities;
using Server.Models.Domain;

namespace Server.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> FindByEmail(string email);

    Task<ApplicationUser?> FindById(string userId);

    Task Create(User user);

    Task Create(User user, string password);
}
