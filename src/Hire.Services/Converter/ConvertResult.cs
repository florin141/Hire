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

    public class Query
    {
        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("quote")]
        public float Quote { get; set; }
    }

}
