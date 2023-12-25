﻿using Newtonsoft.Json.Linq;
using ObjectFilter.Enum;
using ObjectFilter.Model;

namespace ObjectFilter.Functions;

public static class ObjectEvaluator
{
    public static bool ApplyCorrespondingFilter(Dictionary<ObjectType, FilterPredicate> policies, object obj)
    {
        switch (obj)
        {
            case Product product when policies.TryGetValue(ObjectType.Product, out var filter):
                return EvaluateObject(filter, product);

            case Order order when policies.TryGetValue(ObjectType.Order, out var filter):
                return EvaluateObject(filter, order);
            
            default:
                return false;
        }
    }
    
    public static bool EvaluateObject(FilterPredicate filter, object obj)
    {
        switch (filter.Operation.ToLower())
        {
            case "not":
                return EvaluateNot(filter, obj);
            case "and":
                return EvaluateAnd(filter, obj);
            case "or":
                return EvaluateOr(filter, obj);
            case "equals":
                return EvaluateEquals(filter, obj);
            case "contains":
                return EvaluateContains(filter, obj);
            case "greaterthan":
                return EvaluateGreaterThan(filter, obj);
            case "greaterthanorequal":
                return EvaluateGreaterThanOrEqual(filter, obj);
            case "lowerthan":
                return EvaluateLowerThan(filter, obj);
            case "lowerthanorequal":
                return EvaluateLowerThanOrEqual(filter, obj);
            case "null":
                return EvaluateNull(filter, obj);
            case "notnull":
                return !EvaluateNull(filter, obj);
            case "empty":
                return EvaluateEmpty(filter, obj);
            case "notempty":
                return !EvaluateEmpty(filter, obj);
            default:
                throw new InvalidOperationException($"Unsupported filter operation: {filter.Operation}");
        }
    }

    private static bool EvaluateNot(FilterPredicate filter, object obj)
    {
        return !EvaluateOr(filter, obj);
    }
    
    private static bool EvaluateAnd(FilterPredicate filter, object obj)
    {
        return filter.Apply.All(subFilter => EvaluateObject(subFilter, obj));
    }

    private static bool EvaluateOr(FilterPredicate filter, object obj)
    {
        return filter.Apply.Any(subFilter => EvaluateObject(subFilter, obj));
    }

    private static bool EvaluateEquals(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);
        return Equals(propertyValue, filter.Value);
    }

    private static bool EvaluateContains(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is string stringPropertyValue)
        {
            return stringPropertyValue.Contains(filter.Value.ToString());
        }

        if (propertyValue is IEnumerable<object> enumerablePropertyValue)
        {
            return enumerablePropertyValue.Contains(filter.Value);
        }

        return false;
    }

    private static bool EvaluateGreaterThan(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is IComparable comparableValue)
        {
            return comparableValue.CompareTo(filter.Value) > 0;
        }

        throw new InvalidOperationException($"Cannot compare non-comparable type for GreaterThan: {filter.Path}");
    }

    private static bool EvaluateGreaterThanOrEqual(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is IComparable comparableValue)
        {
            return comparableValue.CompareTo(filter.Value) >= 0;
        }

        throw new InvalidOperationException(
            $"Cannot compare non-comparable type for GreaterThanOrEqual: {filter.Path}");
    }

    private static bool EvaluateLowerThan(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is IComparable comparableValue)
        {
            return comparableValue.CompareTo(filter.Value) < 0;
        }

        throw new InvalidOperationException($"Cannot compare non-comparable type for LowerThan: {filter.Path}");
    }

    private static bool EvaluateLowerThanOrEqual(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is IComparable comparableValue)
        {
            return comparableValue.CompareTo(filter.Value) <= 0;
        }

        throw new InvalidOperationException($"Cannot compare non-comparable type for LowerThanOrEqual: {filter.Path}");
    }

    private static bool EvaluateEmpty(FilterPredicate filter, object obj)
    {
        var result = EvaluateNull(filter, obj);
        
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue is string stringValue)
        {
            result = string.IsNullOrEmpty(stringValue);
        }

        if (propertyValue is IEnumerable<object> enumerableValue)
        {
            result = !enumerableValue.Any();
        }

        return result;
    }

    private static bool EvaluateNull(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);
        
        if (propertyValue == null)
        {
            return true;
        }

        return false;
    }

    public static object GetPropertyValue(object obj, string jsonPath)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(jsonPath);

        if (token == null)
        {
            throw new ArgumentException($"Invalid JSONPath expression: {jsonPath}");
        }
        
        return token is JArray ? token.ToObject<List<object>>() : token.ToObject<object>();
    }
}