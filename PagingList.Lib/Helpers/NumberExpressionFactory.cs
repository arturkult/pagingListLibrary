using System;
using System.Linq.Expressions;
using System.Reflection;
using PagingList.Lib.Exceptions;

namespace PagingList.Lib.Helpers;

public static class NumberExpressionFactory
{
    public static Expression<Func<T, bool>> CreateExpression<T>(PropertyInfo property, ParameterExpression type,
        string filterOperator, string value)
    {
        var expressionProperty = Expression.Property(type, property);
        return Expression.Lambda<Func<T, bool>>(
            GetNumberOperator(filterOperator, 
                expressionProperty, 
                Expression.Constant(ParseValue(value, property.PropertyType), property.PropertyType)),
            type
        );
    }

    private static object? ParseValue(string value, Type propertyType)
        => propertyType.GetMethod("Parse", new[]{typeof(string)})?.Invoke(null, new object[] { value });

    private static Expression GetNumberOperator(string filterOperator, Expression property, Expression value)
    {
        return filterOperator switch
        {
            "<" => Expression.LessThan(property, value),
            ">" => Expression.GreaterThan(property, value),
            "=" => Expression.Equal(property, value),
            "<=" => Expression.LessThanOrEqual(property, value),
            ">=" => Expression.GreaterThanOrEqual(property, value),
            _ => throw new FilterOperatorNotSupportedException()
        };
    }
}