using System.Reflection;
using ObjectFilter.Model;

namespace ObjectFilter.Helper;

public class ObjectFilterHelper
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

    private static bool EvaluateEquals(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path);
        return Equals(propertyValue, filter.Value);
    }

    private static bool EvaluateContains(FilterPredicate filter, object obj)
    {
        var propertyValue = GetPropertyValue(obj, filter.Path) as IEnumerable<string>;
        return propertyValue?.Contains(filter.Value.ToString()) == true;
    }

    private static bool EvaluateAnd(FilterPredicate filter, object obj)
    {
        return filter.Apply.All(subFilter => EvaluateFilter(subFilter, obj));
    }

    private static bool EvaluateOr(FilterPredicate filter, object obj)
    {
        return filter.Apply.Any(subFilter => EvaluateFilter(subFilter, obj));
    }

    public static object GetPropertyValue(object obj, string path)
    {
        var propertyNames = path.Trim('/').Split('/');
        var currentObject = obj;

        // foreach (var propertyName in propertyNames)
        // {
        //     var property = currentObject?.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase);
        //
        //     if (property == null)
        //     {
        //         // Property not found, return a default value
        //         return null; // Or you can return a specific default value based on the property type
        //     }
        //
        //     currentObject = property.GetValue(currentObject);
        // }
        
        //Console.WriteLine(currentObject.GetType());

        return currentObject;
    }
}