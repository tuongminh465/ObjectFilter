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
                WarrantyType = "Normal"
            }
        };
        
        var order = new Order
        {
            OrderId = "ext-order-001",
            OrderNumber = 001
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
                    Value = "brand-21"
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
                            Value = "ext-var-3"
                        },
                        new()
                        {
                            Operation = "GreaterThan",
                            Path = "$.Warranty.DurationInMonth",
                            Value = 12
                        }
                    }
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filterPredicate, product);
        Console.WriteLine(result.ToString());
        
        var channelMapping = new ChannelMapping
        {
            SourceId = "Source123",
            TargetId = "Target456",
            Policies = new Dictionary<ObjectType, FilterPredicate>
            {
                {
                    ObjectType.Product,
                    new FilterPredicate
                    {
                        Operation = "Contains",
                        Path = "$.BrandId",
                        Value = "ext-brand-21",
                        Apply = new List<FilterPredicate>
                        {
                            new FilterPredicate
                            {
                                Operation = "Contains",
                                Path = "$.Warranty.WarrantyType",
                                Value = "NewType"
                            }
                        }
                    }
                },
                {
                    ObjectType.Order,
                    new FilterPredicate
                    {
                        Operation = "Contains",
                        Path = "$.OrderId",
                        Value = "ext-order-001",
                        Apply = null
                    }
                }
                // Add more policies as needed
            }
        };

        var result1 = channelMapping.ApplyFilter(product);
        var result2 = channelMapping.ApplyFilter(order);

        Console.WriteLine(result1);
        Console.WriteLine(result2);
    }
}