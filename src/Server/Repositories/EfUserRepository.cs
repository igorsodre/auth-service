using AutoMapper;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Server.Exceptions;
using Server.Interfaces;
using Server.Models.Domain;

namespace Server.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly IAccountManager _accountManager;
    private readonly IMapper _mapper;

    public EfUserRepository(IAccountManager accountManager, IMapper mapper)
    {
        _accountManager = accountManager;
        _mapper = mapper;
    }

    public async Task<ApplicationUser?> FindByEmail(string email)
    {
        return await _accountManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> FindById(string userId)
    {
        return await _accountManager.FindByIdAsync(userId);
    }

    public async Task Create(User user)
    {
        var identityUser = _mapper.Map<ApplicationUser>(user);
        var result = await _accountManager.CreateAsync(identityUser);

        if (result.Succeeded)
        {
            return;
        }

        var errorMessages = result.Errors.Map(error => error.Description).ToList();
        throw new UserCreationFailed(errorMessages);
    }

    public async Task Create(User user, string password)
    {
        var identityUser = _mapper.Map<ApplicationUser>(user);
        var result = await _accountManager.CreateAsync(identityUser, password);

        if (result.Succeeded)
        {
            return;
        }

        var errorMessages = result.Errors.Map(error => error.Description).ToList();
        throw new UserCreationFailed(errorMessages);
    }
}
