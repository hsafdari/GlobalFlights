using GlobalFlights.API.Models;
using GlobalFlights.API.Services;
using GlobalFlights.Application.Feature.Flight.Query;
using GlobalFlights.Common.Enums;
using GlobalFlights.DTOs.Search;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace GlobalFlights.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _mediar;
        private readonly SSEService _sseService;
        public FlightController(IMediator mediar, SSEService sseService)
        {
            _mediar = mediar;
            _sseService = sseService;
        }       
        [HttpGet("stream")]
        public async Task FlightStreaming(CancellationToken cancellationToken = default)
        {
            try
            {
                _sseService.ConfigureResponse(Response);
                // Send initial connection event               
                await _sseService.SendEventAsync(Response, new SSEEvent
                {
                    EventType = "connection_established",
                    Data = new { message = string.Empty, timestamp = DateTime.UtcNow }
                });
                SearchFlightRequestDto requestDto = new SearchFlightRequestDto(Origin: "BCN", DepartureDate: "2025-11-02", OneWay: true,
                                         Duration: null,
                                         NonStop: true,
                                         ViewBy: ResultViewBy.DESTINATION,
                                         MaxPrice: null,
                                         IncludedAirlineCodes: null,
                                         ExcludedAirlineCodes: null,
                                         TravelClass: null,
                                         CurrencyCode: "EUR",
                                         ProviderCode: null);
                
                await foreach (var flightitem in _mediar.CreateStream(new SearchFlightRequestQuery(requestDto), cancellationToken))
                {
                    await _sseService.SendEventAsync(Response, new SSEEvent
                    {
                        EventType = "flights",
                        Data = new {id=Guid.NewGuid(), message = flightitem, timestamp = DateTime.UtcNow }
                    });
                    await Response.Body.FlushAsync(cancellationToken);           
                }             
                // Optional: send a final event
                await _sseService.SendEventAsync(Response, new SSEEvent
                {
                    EventType = "finish",
                    Data = new { message = string.Empty, timestamp = DateTime.UtcNow }
                });
                await Response.Body.FlushAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _sseService.SendEventAsync(Response, new SSEEvent
                {
                    EventType = "error",
                    Data = new
                    {
                        service = "Error service",
                        error = ex.Message
                    }
                });
            }

        }
    }
}
