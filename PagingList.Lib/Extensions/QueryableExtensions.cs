using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PagingList.Lib.Exceptions;
using PagingList.Lib.Helpers;
using PagingList.Lib.Model;

namespace PagingList.Lib.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    ///     Create paginated list
    /// </summary>
    /// <param name="queryable">source queryable collection</param>
    /// <param name="query">query</param>
    /// <typeparam name="T">type of items</typeparam>
    /// <returns>PagedResult includes filtered items and paging infos</returns>
    /// <exception cref="PropertyNotFoundException"></exception>
    /// <exception cref="TypeNotSupportedException"></exception>
    /// <exception cref="FilterOperatorNotSupportedException"></exception>
    public static PagedResult<T> Paginate<T>(this IQueryable<T> queryable, PagedQuery query)
    {
        var result = queryable;
        var type = Expression.Parameter(typeof(T));


        if (query.FilterBy?.Count > 0)
        {
            foreach (var filter in query.FilterBy)
            {
                var propertyName = filter[0];
                var filterOperator = filter[1];
                var value = filter[2];
                var property = typeof(T).GetProperty(propertyName);
                if (property is null)
                {
                    throw new PropertyNotFoundException();
                }

                result = result.Where(GetOperator<T>(property, filterOperator, value, type));
            }
        }

        if (query.OrderBy?.Length > 0)
        {
            var propertyName = query.OrderBy[0];
            var isDescending = query.OrderBy.Length > 1 && query.OrderBy[1] == "desc";
            var property = typeof(T).GetProperty(propertyName);
            if (property is null)
            {
                throw new PropertyNotFoundException();
            }

            var parameterExpression = Expression.Parameter(typeof(T), propertyName);
            var propertyExpression = Expression.Property(parameterExpression, property);
            var orderByExpression = Expression.Lambda<Func<T, dynamic>>(propertyExpression, parameterExpression);
            result = isDescending ? result.OrderByDescending(orderByExpression) : result.OrderBy(orderByExpression);
        }

        var count = result.Count();
        result = result
            .Skip(query.ResultsPerPage * (query.Page - 1))
            .Take(query.ResultsPerPage);
        return new PagedResult<T>
        {
            Items = result,
            Page = query.Page,
            TotalCount = count,
            ResultsPerPage = query.ResultsPerPage
        };
    }

    private static Expression<Func<T, bool>> GetOperator<T>(PropertyInfo property,
        string filterOperator, string value, ParameterExpression type)
    {
        return property.PropertyType switch
        {
            { } stringType when stringType == typeof(string) => StringExpressionFactory.CreateExpression<T>(property,
                filterOperator, value),
            { } numberType when IsNumber(numberType) => NumberExpressionFactory.CreateExpression<T>(property, type,
                filterOperator, value),
            _ => throw new TypeNotSupportedException()
        };
    }

    private static bool IsNumber(Type type) =>
        type == typeof(sbyte)
        || type == typeof(byte)
        || type == typeof(short)
        || type == typeof(ushort)
        || type == typeof(int)
        || type == typeof(uint)
        || type == typeof(long)
        || type == typeof(ulong)
        || type == typeof(float)
        || type == typeof(double)
        || type == typeof(decimal);
}