using System;
using System.Linq.Expressions;
using System.Reflection;
using PagingList.Lib.Exceptions;

namespace PagingList.Lib.Helpers;

internal static class StringExpressionFactory
{
    internal static Expression<Func<T, bool>> CreateExpression<T>(PropertyInfo property,
        string filterOperator, string value)
    {
        var type = Expression.Parameter(typeof(T));
        var method = GetStringOperator(filterOperator);
        var body = Expression.Call(
            Expression.Property(type, property),
            method,
            Expression.Constant(value));
        return Expression.Lambda<Func<T, bool>>(body, type);
    }
    
    private static MethodInfo GetStringOperator(string filterOperator) =>
        filterOperator switch
        {
            "contains" => typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
            "=" => typeof(string).GetMethod("Equals", new[] { typeof(string) })!,
            "endsWith" => typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!,
            "startsWith" => typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!,
            _ => throw new FilterOperatorNotSupportedException()
        };
}