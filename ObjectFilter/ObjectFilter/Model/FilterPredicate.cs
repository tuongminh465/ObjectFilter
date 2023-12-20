using System.Reflection;

namespace ObjectFilter.Model;

public class FilterPredicate
{
    public string Operation { get; set; }
    public string ObjectType { get; set; }
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

    private static object GetPropertyValue(object obj, string path)
    {
        var propertyNames = path.Split('/');
        var currentObject = obj;

        foreach (var propertyName in propertyNames)
        {
            var property = currentObject?.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase);

            if (property == null)
            {
                // Property not found, return a default value
                return null; // Or you can return a specific default value based on the property type
            }

            currentObject = property.GetValue(currentObject);
        }

        return currentObject;
    }
}