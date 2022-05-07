using System.ComponentModel.DataAnnotations.Schema;

namespace Hire.Core
{
    /// <summary>
    /// Base entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
