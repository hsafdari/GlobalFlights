namespace GlobalFlights.API.Models
{
    public class SSEEvent
    {
        public string EventType { get; set; } = string.Empty;
        public object Data { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
