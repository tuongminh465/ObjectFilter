using Newtonsoft.Json.Linq;
using ObjectFilter.Model;

namespace FilterObject.Functions;

public class ObjectFilterFunction
{
    public static bool EvaluateFilter(FilterPredicate filter, object obj)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

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
            case "empty":
                return EvaluateEmpty(filter, obj);
            case "notempty":
                return EvaluateNotEmpty(filter, obj);
            default:
                throw new InvalidOperationException($"Unsupported filter operation: {filter.Operation}");
        }
    }
    
    private static bool EvaluateNotEmpty(FilterPredicate filter, object obj)
    {
        return !EvaluateEmpty(filter, obj);
    }
    
    private static bool EvaluateNot(FilterPredicate filter, object obj)
    {
        return !EvaluateOr(filter, obj);
    }
    
    private static bool EvaluateAnd(FilterPredicate filter, object obj)
    {
        return filter.Apply.All(subFilter => EvaluateFilter(subFilter, obj));
    }

    private static bool EvaluateOr(FilterPredicate filter, object obj)
    {
        return filter.Apply.Any(subFilter => EvaluateFilter(subFilter, obj));
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
        var propertyValue = GetPropertyValue(obj, filter.Path);

        if (propertyValue == null)
        {
            return true;
        }

        if (propertyValue is string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        if (propertyValue is IEnumerable<object> enumerableValue)
        {
            return !enumerableValue.Any();
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

        return token.ToObject<object>();
    }
}