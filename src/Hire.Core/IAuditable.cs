using System;

namespace Hire.Core
{
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity update
        /// </summary>
        DateTimeOffset? UpdatedOn { get; set; }
    }
}
