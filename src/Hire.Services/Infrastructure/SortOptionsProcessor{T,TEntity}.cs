using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hire.Services.Infrastructure
{
    public class SortOptionsProcessor<T, TEntity>
    {
        private readonly string[] _orderBy;

        public SortOptionsProcessor(string[] orderBy)
        {
            _orderBy = orderBy;
        }

        public IEnumerable<SortTerm> GetAllTerms()
        {
            if (_orderBy == null)
            {
                yield break;
            }

            foreach (var term in _orderBy)
            {
                if (string.IsNullOrEmpty(term))
                {
                    continue;
                }

                var tokens = term.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SortTerm { Name = term };
                    continue;
                }

                var descending = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                yield return new SortTerm
                {
                    Name = tokens[0],
                    Descending = descending
                };
            }
        }

        public IEnumerable<SortTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms().ToArray();
            if (!queryTerms.Any())
            {
                yield break;
            }

            var declaredTerms = GetTermsFromModel()
                .ToArray();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms
                    .SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));
                if (declaredTerm == null)
                {
                    continue;
                }

                yield return new SortTerm
                {
                    Name = declaredTerm.Name,
                    Descending = term.Descending,
                    Default = declaredTerm.Default
                };
            }
        }

        private static IEnumerable<SortTerm> GetTermsFromModel()
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
                .Select(p => new SortTerm
                {
                    Name = p.Name,
                    Default = p.GetCustomAttribute<SortableAttribute>()!.Default
                });
        }

        public IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query)
        {
            var terms = GetValidTerms().ToArray();

            if (!terms.Any())
            {
                terms = GetTermsFromModel()
                    .Where(x => x.Default)
                    .ToArray();
            }

            if (!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // TODO: query = query.OrderBy(x => x.Property);

                // x => x.Property
                var key = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper.GetLambda(typeof(TEntity), propertyInfo.PropertyType, obj, key);

                // query.OrderBy/ThenBy[Descending](x => x.Property);
                modifiedQuery = ExpressionHelper.CallOrderByOrThenBy(modifiedQuery, useThenBy, term.Descending, 
                    propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }
    }
}
