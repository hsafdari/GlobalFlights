using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobalFlights.Common.Enums;

namespace GlobalFlights.DTOs.Search
{
    // Required parameters
    // Optional filters
    // Flight filters
    /// <param name="Origin"></param>
    /// <param name="DepartureDate"></param>
    /// <param name="OneWay"></param>
    /// <param name="Duration"> Exact duration or range of durations of the travel, in days.
    /// This parameter must not be set if oneWay is true.
    ///Ranges are specified with a comma and are inclusive, e.g. 2,8
    ///Duration can not be lower than 1 days or higher than 15 days </param>
    /// <param name="NonStop"></param>
    /// <param name="ViewBy"></param>
    /// <param name="MaxPrice"></param>
    /// <param name="IncludedAirlineCodes"></param>
    /// <param name="ExcludedAirlineCodes"></param>
    /// <param name="TravelClass"></param>
    /// <param name="CurrencyCode"></param>
    /// <param name="ProviderCode"></param>
    public record SearchFlightRequestDto(string Origin, string DepartureDate, bool? OneWay, int? Duration, bool? NonStop, ResultViewBy? ViewBy, int? MaxPrice, string[]? IncludedAirlineCodes, string[]? ExcludedAirlineCodes, string? TravelClass, string CurrencyCode, string? ProviderCode)
    {
        public string ToQueryString()
        {
            var query = new List<string>();
            if (!string.IsNullOrEmpty(Origin)) query.Add($"origin={Origin}");
            if (!string.IsNullOrEmpty(DepartureDate)) query.Add($"departureDate={DepartureDate}");
            if (OneWay.HasValue) query.Add($"oneWay={OneWay.Value.ToString().ToLower()}");
            if (Duration.HasValue) query.Add($"duration={Duration}");
            if (NonStop.HasValue) query.Add($"nonStop={NonStop.Value.ToString().ToLower()}");
            if (ViewBy != null) query.Add($"viewBy={ViewBy}");
            if (MaxPrice.HasValue) query.Add($"maxPrice={MaxPrice}");
            if (IncludedAirlineCodes?.Length > 0) query.Add($"includedAirlineCodes={string.Join(",", IncludedAirlineCodes)}");
            if (ExcludedAirlineCodes?.Length > 0) query.Add($"excludedAirlineCodes={string.Join(",", ExcludedAirlineCodes)}");
            if (!string.IsNullOrEmpty(TravelClass)) query.Add($"travelClass={TravelClass}");
            if (!string.IsNullOrEmpty(CurrencyCode)) query.Add($"currencyCode={CurrencyCode}");

            return string.Join("&", query);
        }
    }
}
