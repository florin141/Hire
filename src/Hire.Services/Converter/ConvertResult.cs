using System.Text.Json.Serialization;

namespace Hire.Services.Converter
{
    public class ConvertResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("query")]
        public Query Query { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("result")]
        public float Result { get; set; }
    }
}
