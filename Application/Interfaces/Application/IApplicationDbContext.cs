
using Domain.Entities.Destination;
using Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Application
{
    public interface IApplicationDbContext
    {
        #region Permission

        DbSet<Permission> Permission { get;}

        #endregion

        #region Destination

        public DbSet<City> Cities { get;}
        public DbSet<Country> Countries { get;}

        #endregion
    }
}
