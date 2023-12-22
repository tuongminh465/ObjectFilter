using Newtonsoft.Json.Linq;
using ObjectFilter.Model;

namespace ObjectFilter;

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
                WarrantyType = "Normal",
                Testing = new Testing()
                {
                    Example = "Nice work"
                }
            }
        };

        // var propertyAccessor = new PropertyAccessor(product.GetType());

        // Example 1
        object? result1 = GetValue(product, "$.Color");
        Console.WriteLine(result1);

        // Example 2
        object? result2 = GetValue(product, "$.Warranty.DurationInMonths");
        Console.WriteLine(result2);
        
        //Example 3
        object? result3 = GetValue(product, "$.Warranty.Testing.Example");
        Console.WriteLine(result3);
    }

    private static object? GetValue(object obj, string path)
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