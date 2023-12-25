using ObjectFilter.Enum;
using ObjectFilter.Functions;

namespace ObjectFilter.Model;

public class ChannelMapping
{
    public ChannelMapping()
    {
        Id = Guid.NewGuid().ToString();
        Policies = new Dictionary<ObjectType, FilterPredicate>();
    }

    public string Id { get; set; }
    public Dictionary<ObjectType, FilterPredicate> Policies { get; set; }
    public string SourceId { get; set; }
    public string TargetId { get; set; }
    
    public bool ApplyFilter(object obj)
    {
        foreach (var kvp in Policies)
        {
            ObjectType objectType = kvp.Key;
            FilterPredicate filterPredicate = kvp.Value;

            // Check the object type and apply the filter predicate
            switch (objectType)
            {
                case ObjectType.Product:
                    if (obj is Product product)
                    {
                       return ApplyFilterForProduct(kvp.Value, product);
                    }
                    break;
                case ObjectType.Order:
                    if (obj is Order order)
                    {
                        return ApplyFilterForOrder(kvp.Value, order);
                    }
                    break;
                // Add more cases for other object types if needed

                default:
                    throw new InvalidOperationException($"Unsupported ObjectType: {kvp.Key}");
            }
        }

        return false;
    }
    
    private static bool ApplyFilterForProduct(FilterPredicate filter, Product product)
    {
        // Apply filter for Product object
        return ObjectEvaluator.EvaluateObject(filter, product);
    }
    
    private static bool ApplyFilterForOrder(FilterPredicate filter, Order order)
    {
        // Apply filter for Order object
        return ObjectEvaluator.EvaluateObject(filter, order);
    }

}