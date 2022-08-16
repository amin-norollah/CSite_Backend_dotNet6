using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CSite.Shared.Extensions
{
    public static class QueryableExtensions
    {

        public static IQueryable<T> If<T>(
            this IQueryable<T> source,
            bool condition,
            Func<IQueryable<T>, IQueryable<T>> transform
        )
        {
            return condition ? transform(source) : source;
        }

        public static IQueryable<T> If<T, P>(
          this IQueryable<T> source,
          bool condition,
          Expression<Func<T, P>> navigationPropertyPath
        )
          where T : class
        {
            return condition ? source.Include(navigationPropertyPath) : source;
        }

        public static IQueryable<T> If<T, P>(
            this IIncludableQueryable<T, P> source,
            bool condition,
            Func<IIncludableQueryable<T, P>, IQueryable<T>> transform
        )
            where T : class
        {
            return condition ? transform(source) : source;
        }

        public static IQueryable<T> If<T, P>(
            this IIncludableQueryable<T, IEnumerable<P>> source,
            bool condition,
            Func<IIncludableQueryable<T, IEnumerable<P>>, IQueryable<T>> transform
        )
            where T : class
        {
            return condition ? transform(source) : source;
        }

    }
}
