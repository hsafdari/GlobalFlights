using GlobalFlights.API.Models;
using System.Text;
using System.Text.Json;

namespace GlobalFlights.API.Services
{
    public class SSEService
    {
        private readonly ILogger<SSEService> _logger;

        public SSEService(ILogger<SSEService> logger)
        {
            _logger = logger;
        }

        public async Task SendEventAsync(HttpResponse response, SSEEvent sseEvent)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(sseEvent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var eventData = $"data: {jsonData}\n\n";

                await response.Body.WriteAsync(Encoding.UTF8.GetBytes(eventData));
                //await response.WriteAsync(eventData);
                await response.Body.FlushAsync();

                _logger.LogInformation("Sent SSE event: {EventType}", sseEvent.EventType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending SSE event");
            }
        }

        public void ConfigureResponse(HttpResponse response)
        {
            response.Headers.Append("Content-Type", "text/event-stream");
            response.Headers.Append("Cache-Control", "no-cache");
            response.Headers.Append("Connection", "keep-alive");
            response.Headers.Append("Access-Control-Allow-Origin", "*");
        }
    }
}
