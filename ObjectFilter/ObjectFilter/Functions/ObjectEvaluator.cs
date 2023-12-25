using Newtonsoft.Json.Linq;
using ObjectFilter.Enum;
using ObjectFilter.Model;
using Qilin.Core.QilinShared.Common.Constants;

namespace ObjectFilter.Functions;

public static class ObjectEvaluator
{
    // TODO: move to ChannleMapping or something idk
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
            // TODO: make all operations const values
            case FilterPredicateOperator.Not:
                return EvaluateNot(filter, obj);
            case FilterPredicateOperator.And:
                return EvaluateAnd(filter, obj);
            case FilterPredicateOperator.Or:
                return EvaluateOr(filter, obj);
            case FilterPredicateOperator.Equals:
                return EvaluateEquals(filter, obj);
            case FilterPredicateOperator.Contains:
                return EvaluateContains(filter, obj);
            case FilterPredicateOperator.ArrayContains:
                return EvaluateArrayContains(filter, obj);
            case FilterPredicateOperator.GreaterThan:
                return EvaluateGreaterThan(filter, obj);
            case FilterPredicateOperator.GreaterThanOrEqual:
                return EvaluateGreaterThanOrEqual(filter, obj);
            case FilterPredicateOperator.LowerThan:
                return EvaluateLowerThan(filter, obj);
            case FilterPredicateOperator.LowerThanOrEqual:
                return EvaluateLowerThanOrEqual(filter, obj);
            case FilterPredicateOperator.Null:
                return EvaluateNull(filter, obj);
            case FilterPredicateOperator.NotNull:
                return !EvaluateNull(filter, obj);
            case FilterPredicateOperator.Empty:
                return EvaluateEmpty(filter, obj);
            case FilterPredicateOperator.ArrayEmpty:
                return EvaluateArrayEmpty(filter, obj);
            case FilterPredicateOperator.NotEmpty:
                return !EvaluateEmpty(filter, obj);
            case FilterPredicateOperator.ArrayNotEmpty:
                return !EvaluateArrayEmpty(filter, obj);
            default:
                throw new InvalidOperationException($"Unsupported filter operation: {filter.Operation}");
        }
    }

    private static bool EvaluateNot(FilterPredicate filter, object obj)
    {
        // TODO: Only allow 1 operation in Apply in validation 
        return !EvaluateOr(filter, obj);
    }
    
    private static bool EvaluateAnd(FilterPredicate filter, object obj)
    {
        // TODO: Allow >= 2 apply operations in validation 
        return filter.Apply.All(subFilter => EvaluateObject(subFilter, obj));
    }

    private static bool EvaluateOr(FilterPredicate filter, object obj)
    {
        // TODO: Allow >= 2 apply operations in validation 
        return filter.Apply.Any(subFilter => EvaluateObject(subFilter, obj));
    }

    private static bool EvaluateEquals(FilterPredicate filter, object obj)
    {
        if (EvaluateNull(filter, obj))
        {
            return false;
        }
        
        var propertyValue = GetPropertyValue(obj, filter.Path);
        
        return Equals(propertyValue, filter.Value);
    }

    private static bool EvaluateContains(FilterPredicate filter, object obj)
    {
        if (EvaluateNull(filter, obj))
        {
            return false;
        }
        
        var propertyValue = GetPropertyValue(obj, filter.Path) as string;
        
        return propertyValue.Contains(filter.Value.ToString());
    }
    
    private static bool EvaluateArrayContains(FilterPredicate filter, object obj)
    {
        if (EvaluateNull(filter, obj))
        {
            return false;
        }
        
        var propertyValue = GetArrayValue(obj, filter.Path);
        
        return propertyValue.Contains(filter.Value);
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
        if (EvaluateNull(filter, obj))
        {
            return true;
        }
        
        var propertyValue = GetPropertyValue(obj, filter.Path) as string;

        return string.IsNullOrEmpty(propertyValue);
    }
    
    private static bool EvaluateArrayEmpty(FilterPredicate filter, object obj)
    {
        if (EvaluateNull(filter, obj))
        {
            return true;
        }
        
        var propertyValue = GetArrayValue(obj, filter.Path);

        return !propertyValue.Any();
    }

    private static bool EvaluateNull(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);
        
        return propertyValue == null;
    }

    public static object? GetPropertyValue(object obj, string jsonPath)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(jsonPath, true);
        
        return token?.ToObject<object>();
    }
    
    public static IEnumerable<object>? GetArrayValue(object obj, string jsonPath)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(jsonPath, true);
        
        return token?.ToObject<List<object>>();
    }
}