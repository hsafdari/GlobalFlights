using GlobalFlights.Application.Feature.Flight.Query;
using GlobalFlights.DTOs.Search;
using GlobalFlights.ExternalServices.Interfaces;
using MediatR;
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
        private readonly IEnumerable<IApiService> _apiServices;      
        public SearchFlightRequestQueryHandler(IEnumerable<IApiService> apiServices)
        {
            _apiServices = apiServices;
        }
        public async IAsyncEnumerable<SearchFlightResponseDto> Handle(SearchFlightRequestQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            //var _taskLIsts = new List<Task<IAsyncEnumerable<SearchFlightResponseDto>>>();
            var _tasks = _apiServices.Select(x => x.SearchRequestToAPIAsync(request.requestDto)).ToList();
            var enumerators = _tasks.Select(t => t.GetAsyncEnumerator(cancellationToken)).ToList();           

            //while (_tasks.Any())
            //{
            //    var finished = await Task.WhenAny(_tasks);
            //    _tasks.Remove(finished);
            //    yield return await finished;
            //}            
            //try
            //{
            //    bool done = false;
            //    int i = 1;
            //    do
            //    {
            //        foreach (var item in enamators)
            //        {
            //            if (await item.MoveNextAsync())
            //            {
            //                done = false;
            //                yield return item.Current;
            //                Console.WriteLine("number:" + i);
            //            }
            //        }
            //    }
            //    while (!done && !cancellationToken.IsCancellationRequested);
            //}
            //finally
            //{
            //    int i = 1;
            //    foreach (var _task in enamators)
            //    {
            //        await _task.DisposeAsync();
            //        Console.WriteLine("Dispose number:" + i);
            //    }                
            //}
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
