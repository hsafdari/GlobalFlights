using GlobalFlights.Application.Feature.Flight.Query;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.Application.Feature.Flight.Handler
{
    public class SearchFlightRequestQueryHandler : IStreamRequestHandler<SearchFlightRequestQuery,SearchFlightResponseDto>
    {
        private readonly IEnumerable<IExternalApiService> _apiServices;      
        public SearchFlightRequestQueryHandler(IEnumerable<IExternalApiService> apiServices)
        {
            _apiServices = apiServices;
        }
        public async IAsyncEnumerable<SearchFlightResponseDto> Handle(SearchFlightRequestQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {          
            var _tasks = _apiServices.Select(x => x.FetchDataAsync(request.requestDto)).ToList();
            var enumerators = _tasks.Select(t => t.GetAsyncEnumerator(cancellationToken)).ToList();                      
            try
            {
                while (enumerators.Count > 0 && !cancellationToken.IsCancellationRequested)
                {
                    // Track which enumerators have completed
                    var completed = new List<IAsyncEnumerator<SearchFlightResponseDto>>();

                    foreach (var en in enumerators)
                    {
                        // If this enumerator can still produce items
                        if (await en.MoveNextAsync())
                        {
                            yield return en.Current;
                        }
                        else
                        {
                            // This one finished
                            completed.Add(en);
                        }
                    }

                    // Remove completed enumerators
                    foreach (var c in completed)
                    {
                        await c.DisposeAsync();
                        enumerators.Remove(c);
                    }

                    // Break if all streams are done
                    if (enumerators.Count == 0)
                        break;
                }
            }
            finally
            {
                foreach (var en in enumerators)
                    await en.DisposeAsync();
            }        
        }      
    }
}
