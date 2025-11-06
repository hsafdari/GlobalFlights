using MediatR;
using GlobalFlights.DTOs.Search;

namespace GlobalFlights.Application.Feature.Flight.Query
{
    public record SearchFlightRequestQuery(SearchFlightRequestDto requestDto) : IStreamRequest<SearchFlightResponseDto>;   
}
