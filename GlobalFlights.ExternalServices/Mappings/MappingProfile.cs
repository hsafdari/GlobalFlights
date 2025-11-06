using AutoMapper;
using GlobalFlights.DTOs.Search;
using Amadeus = GlobalFlights.ExternalServices.Providers.Amadeus.Models;
using Lufthansa = GlobalFlights.ExternalServices.Providers.Lufthansa.Models;
using TravelPort = GlobalFlights.ExternalServices.Providers.TravelPort.Models;

namespace GlobalFlights.ExternalServices.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Amadeus.SearchAmadeusResponse, SearchFlightResponseDto>();             
            CreateMap<Amadeus.FlightData, FlightDataDto>();
            CreateMap<Amadeus.Price, PriceDto>();
            CreateMap<Amadeus.Meta, MetaDto>();
            CreateMap<Amadeus.Links, LinksDto>();
            CreateMap<Lufthansa.SearchLufthansaResponse, SearchFlightResponseDto>();
            CreateMap<Lufthansa.FlightData, FlightDataDto>();
            CreateMap<Lufthansa.Price, PriceDto>();
            CreateMap<Lufthansa.Meta, MetaDto>();
            CreateMap<Lufthansa.Links, LinksDto>();
            CreateMap<TravelPort.SearchTravelPortResponse, SearchFlightResponseDto>();
            CreateMap<TravelPort.FlightData, FlightDataDto>();
            CreateMap<TravelPort.Price, PriceDto>();
            CreateMap<TravelPort.Meta, MetaDto>();
            CreateMap<TravelPort.Links, LinksDto>();
        }
    }
}
