using GlobalFlights.API.Models;
using GlobalFlights.API.Services;
using GlobalFlights.Application.Feature.Flight.Query;
using GlobalFlights.Common.Enums;
using GlobalFlights.DTOs.Search;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("search")]
        public async Task<ActionResult> FlightSearch([FromQuery] SearchFlightRequestDto request, CancellationToken cancellation)
        {
            //throw new NotImplementedException();
            //api call=> App=>ExternalServices=>App>api=>web
            var result = await _mediar.Send(new SearchFlightRequestQuery(request), cancellation);
            return Ok(result);
        }
        [HttpGet("stream")]
        public async Task FlightStreaming(CancellationToken cancellationToken = default)
        {
            try
            {
                //_sseService.ConfigureResponse(Response);
                // Send initial connection event
                Response.StatusCode = 200;
                Response.Headers.Append("Content-Type", "text/event-stream");
                Response.Headers.Append("Cache-Control", "no-cache");
                Response.Headers.Append("Connection", "keep-alive");
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
                    //var json = JsonSerializer.Serialize(flightitem);
                    
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
