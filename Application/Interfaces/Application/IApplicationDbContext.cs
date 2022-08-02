
using Domain.Entities.Destination;
using Domain.Entities.Hotels;
using Domain.Entities.Localization;
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

        #region Hotel

        public DbSet<Hotel> Hotels { get;}
        public DbSet<HotelFacility> HotelFacilities { get;}
        public DbSet<HotelSelectedFacility> HotelSelectedFacilities { get;}
        public DbSet<HotelImage> HotelImages { get;}

        #endregion

        #region Localization

        public DbSet<Language> Languages { get;}

        #endregion

        public ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=new());
    }
}
