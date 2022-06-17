using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

internal class DataContext : IdentityDbContext<ApplicationUser>, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}
