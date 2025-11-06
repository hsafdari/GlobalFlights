namespace GlobalFlights.DTOs.Search
{
    public record SearchFlightResponseDto(List<FlightDataDto> Data, MetaDto Meta, string? ProviderCode);

    public record FlightDataDto(string Type, string Origin, string Destination, string DepartureDate, string ReturnDate, PriceDto Price, string Links);

    public record PriceDto(string Currency, decimal Total);

    public record MetaDto(int Count, LinksDto Links);

    public record LinksDto(string Self);
}
