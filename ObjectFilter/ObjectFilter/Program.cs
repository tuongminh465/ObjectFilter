using ObjectFilter.Enum;
using ObjectFilter.Functions;
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
                WarrantyType = null
            }
        };
        
        var order = new Order
        {
            OrderId = "ext-order-001",
            OrderNumber = 001
        };

        var policies = new Dictionary<ObjectType, FilterPredicate>
        {
            {
                ObjectType.Product,
                new FilterPredicate
                {
                    Operation = "Contains",
                    Path = "$.Warranty.WarrantyType",
                    Value = "brand-23",
                }
            },
            {
                ObjectType.Order,
                new()
                {
                    Operation = "Contains",
                    Path = "$.OrderId",
                    Value = "ext-order-001",
                    Apply = null
                }
            }
        };
        
        var result1 = ObjectEvaluator.ApplyCorrespondingFilter(policies, product);
        // var result2 = ObjectEvaluator.ApplyCorrespondingFilter(policies, order);

        Console.WriteLine(result1);
        // Console.WriteLine(result2);
    }
}