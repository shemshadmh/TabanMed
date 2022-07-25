
using Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Application
{
    public interface IApplicationDbContext
    {
        #region Permission

        DbSet<Permission> Permission { get; }

        #endregion
    }
}
