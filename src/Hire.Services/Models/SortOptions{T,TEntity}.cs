using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hire.Services.Infrastructure;

namespace Hire.Services.Models
{
    public class SortOptions<T, TEntity> : IValidatableObject
    {
        public string[] OrderBy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);

            var validTerms = processor.GetValidTerms().Select(x => x.Name);
            var invalidTerms = processor.GetAllTerms().Select(x => x.Name)
                .Except(validTerms, StringComparer.OrdinalIgnoreCase);

            foreach (var invalidTerm in invalidTerms)
            {
                yield return new ValidationResult($"Invalid sort term '{invalidTerm}'.", new[] { nameof(OrderBy) });
            }
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var processor = new SortOptionsProcessor<T, TEntity>(OrderBy);

            return processor.Apply(query);
        }
    }
}
