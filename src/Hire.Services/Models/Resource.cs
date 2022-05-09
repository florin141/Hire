using Newtonsoft.Json;

namespace Hire.Services.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
