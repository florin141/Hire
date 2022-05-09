using System.Text.Json.Serialization;

namespace Hire.Services.Converter
{
    public class Info
    {
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("quote")]
        public float Quote { get; set; }
    }
}