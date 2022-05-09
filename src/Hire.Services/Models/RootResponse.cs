namespace Hire.Services.Models
{
    public class RootResponse : Resource
    {
        public Link Orders { get; set; }

        public Link Vehicles { get; set; }
    }
}
