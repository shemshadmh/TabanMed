using Application.Common;
using Application.Dtos.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Destination
{
    public  interface ICityApplication : IAsyncDisposable
    {
        Task<IReadOnlyList<CityListItem>?> GetCitiesListAsync(int CountryId);
        Task<OperationResult> CreateCity(CityListItem cityDto);
        Task<OperationResult> UpdateCity(CityListItem cityDto);
    }
}
