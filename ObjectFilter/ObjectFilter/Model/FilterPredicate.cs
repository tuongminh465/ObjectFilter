using Newtonsoft.Json.Linq;

namespace ObjectFilter.Model;

public abstract class FilterPredicate
{
    public string Operation { get; set; }
    public string Path { get; set; }
    public object Value { get; set; }
    public List<FilterPredicate> Apply { get; set; }
}

public class FilterEvaluator<T>
{
    public static bool EvaluateFilter(FilterPredicate filter, T obj)
    {
        if (filter == null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        switch (filter.Operation)
        {
            case "Equals":
                return EvaluateEquals(filter, obj);
            case "Contains":
                return EvaluateContains(filter, obj);
            case "And":
                return EvaluateAnd(filter, obj);
            case "Or":
                return EvaluateOr(filter, obj);
            default:
                throw new InvalidOperationException($"Unsupported filter operation: {filter.Operation}");
        }
    }

    private static bool EvaluateEquals(FilterPredicate filter, T obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);
        return Equals(propertyValue, filter.Value);
    }

    private static bool EvaluateContains(FilterPredicate filter, T obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path) as IEnumerable<string>;
        return propertyValue?.Contains(filter.Value.ToString()) == true;
    }

    private static bool EvaluateAnd(FilterPredicate filter, T obj)
    {
        return filter.Apply.All(subFilter => EvaluateFilter(subFilter, obj));
    }

    private static bool EvaluateOr(FilterPredicate filter, T obj)
    {
        return filter.Apply.Any(subFilter => EvaluateFilter(subFilter, obj));
    }

    private static object? GetPropertyValue(object obj, string path)
    {
        var json = JObject.FromObject(obj);
        var token = json.SelectToken(path);

        if (token == null)
        {
            throw new ArgumentException($"Invalid JSONPath expression: {path}");
        }

        return token.ToObject<object>();
    }
}