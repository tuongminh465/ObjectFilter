using FilterObject.Functions;
using ObjectFilter.Model;

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
                DurationInMonth = 12,
                WarrantyType = "Normal"
            }
        };
        
        var filterPredicate = new FilterPredicate
        {
            Operation = "Not",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-23"
                },
                new()
                {
                    Operation = "Or",
                    Apply = new List<FilterPredicate>
                    {
                        new()
                        {
                            Operation = "Contains",
                            Path = "$.VariationIds",
                            Value = "ext-var-2"
                        },
                        new()
                        {
                            Operation = "GreaterThan",
                            Path = "$.Warranty.DurationInMonths",
                            Value = 12
                        }
                    }
                }
            }
        };

        var result = ObjectFilterFunction.EvaluateFilter(filterPredicate, product);
        Console.WriteLine(result.ToString());
    }
}