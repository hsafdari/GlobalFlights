using GlobalFlights.DTOs.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.ExternalServices.Interfaces
{
    public interface IExternalApiService
    {
        IAsyncEnumerable<SearchFlightResponseDto> FetchDataAsync(SearchFlightRequestDto requestDto);
    }
}
