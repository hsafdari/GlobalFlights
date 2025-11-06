using GlobalFlights.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.Domain.Entity.Search
{
    public class SearchResponse : BaseEntity
    {
        public List<FlightData> Data { get; set; }
        public Meta Meta { get; set; }
        public string ProviderCode { get; set; } //eg ams for amadeus
    }
    public class FlightData
    {
        public string Type { get; set; } // "flight-destination"
        public string Origin { get; set; } // IATA code
        public string Destination { get; set; } // IATA code
        public string DepartureDate { get; set; } // "YYYY-MM-DD"
        public string ReturnDate { get; set; } // "YYYY-MM-DD"
        public Price Price { get; set; }
        public string Links { get; set; } // Optional: may contain booking links
    }

    public class Price
    {
        public string Currency { get; set; } // e.g., "EUR"
        public decimal Total { get; set; } // e.g., 250.00
    }

    public class Meta
    {
        public int Count { get; set; } // Number of results
        public Links Links { get; set; }
    }

    public class Links
    {
        public string Self { get; set; } // URL to the current result set
    }
}
