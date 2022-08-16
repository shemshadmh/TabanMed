using Application.Dtos.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos.Hotels.HotelFacilities;

namespace Application.Interfaces.Destination
{
    public interface ICountryApplication : IAsyncDisposable
    {
        Task<IReadOnlyList<CountryListItem>?> GetCountriesListAsync();
        Task<OperationResult> CreateCountry(CountryListItem countryDto);
    }
}
