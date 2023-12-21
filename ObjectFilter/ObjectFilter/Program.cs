using Newtonsoft.Json.Linq;
using ObjectFilter.Model;

namespace FilterObject;

public class PropertyAccessor
{
    private readonly Type _objectType;

    public PropertyAccessor(Type objectType)
    {
        _objectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
    }

    public object? GetValue(object obj, string jsonPath)
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

class Program
{
    static void Main()
    {
        var product = new Product
        {
            BrandId = "ext-brand-23",
            VariationIds = new List<string> { "ext-var-1", "ext-var-2" },
            Color = "red",
            Warranty = new Warranty
            {
                DurationInMonths = 12,
                WarrantyType = "Normal"
            }
        };

        var propertyAccessor = new PropertyAccessor(product.GetType());

        // Example 1
        object? result1 = propertyAccessor.GetValue(product, "$.Color");
        Console.WriteLine(result1);

        // Example 2
        object? result2 = propertyAccessor.GetValue(product, "$.Warranty.DurationInMonths");
        Console.WriteLine(result2);
    }
}