using GlobalFlights.Common.Entity;
using GlobalFlights.Common.Enums;

namespace GlobalFlights.Domain.Entity.Search
{
    public class SearchRequest : BaseEntity
    {
        // Required parameters
        public required string Origin { get; set; } // IATA code of departure airport (e.g., "MAD")
        public required string DepartureDate { get; set; } // Format: "YYYY-MM-DD"

        // Optional filters
        public bool? OneWay { get; set; } // true or false
        /// <summary>
        /// Exact duration or range of durations of the travel, in days.
        /// This parameter must not be set if oneWay is true.
        ///Ranges are specified with a comma and are inclusive, e.g. 2,8
        ///Duration can not be lower than 1 days or higher than 15 days
        /// </summary>
        public int? Duration { get; set; } // Max trip duration in days
        public bool? NonStop { get; set; } // true or false
        public ResultViewBy? ViewBy { get; set; } // "DATE" or "DURATION"

        // Flight filters
        public int? MaxPrice { get; set; } // e.g., 300
        public string[] IncludedAirlineCodes { get; set; } // e.g., ["IB", "BA"]
        public string[] ExcludedAirlineCodes { get; set; } // e.g., ["FR"]
        public string TravelClass { get; set; } // "ECONOMY", "BUSINESS", etc.
        public string CurrencyCode { get; set; } // e.g., "EUR"
        public string ProvidersCodes { get; set; } //from UI we don't know about active GDS Providers=> in Business we find out about them)
                                                   //seprated by , e.g. Amadeus,Sabre,Travelport,Galileo,Worldspan
                                                   //so in api request can be empty in app we should find actives to request their api
    }
}
