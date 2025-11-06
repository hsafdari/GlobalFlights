using GlobalFlights.ExternalServices.Model;

namespace GlobalFlights.ExternalServices.Providers.Amadeus.Models
{
    // Represents the root response from the Amadeus API
    public record SearchAmadeusResponse(
        List<FlightData> Data,
        Meta Meta,
        string? ProviderCode="AMD"
    );

    // Represents individual flight details within the response
    public record FlightData(
        string Type,         // "flight-destination"
        string Origin,       // IATA code
        string Destination,  // IATA code
        string DepartureDate,// "YYYY-MM-DD"
        string ReturnDate,   // "YYYY-MM-DD"
        Price Price,
        string Links         // Optional: may contain booking links
    );

    // Represents price information
    public record Price(
        string Currency, // e.g., "EUR"
        decimal Total    // e.g., 250.00
    );

    // Represents metadata about the search results
    public record Meta(
        int Count, // Number of results
        Links Links
    );

    // Represents navigation links within the metadata
    public record Links(
        string Self // URL to the current result set
    );
}
