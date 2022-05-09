using System;

namespace Hire.Services.Models
{
    public class User : Resource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Phone { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
