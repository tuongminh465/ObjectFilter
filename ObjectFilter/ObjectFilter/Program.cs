using ObjectFilter.Model;

namespace ObjectFilter;

static class Program
{
    private static void Main()
    {
        // Example Product instance
        var product = new Product
        {
            BrandId = "ext-brand-23",
            VariationIds = new List<string> { "ext-var-1", "ext-var-2" },
            Color = "red"
        };

        
        
        // Example FilterPredicate instance
        var filterPredicate = new FilterPredicate
        {
            Operation = "And",
            ObjectType = "Product",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Equals",
                    ObjectType = "Product",
                    Path = "/brandId",
                    Value = "ext-brand-23"
                },
                new()
                {
                    Operation = "Or",
                    ObjectType = "Product",
                    Apply = new List<FilterPredicate>
                    {
                        new()
                        {
                            Operation = "Contains",
                            ObjectType = "Product",
                            Path = "/variationIds",
                            Value = "ext-var-2"
                        },
                        new()
                        {
                            Operation = "Equals",
                            ObjectType = "Product",
                            Path = "/color",
                            Value = "red"
                        }
                    }
                }
            }
        };
        
        // Evaluate the filter for the product
        bool productResult = FilterEvaluator<Product>.EvaluateFilter(filterPredicate, product);
        
        // Output the result for the product
        Console.WriteLine($"Product satisfies the filter: {productResult}");

        //Example Person instance
        var person = new Person
        {
            Name = "John Doe",
            Age = 30,
            City = "New York"
        };
        
        // Example FilterPredicate for Person
        var personFilter = new FilterPredicate
        {
            Operation = "And",
            ObjectType = "Person",
            Apply = new List<FilterPredicate>
            {
                new FilterPredicate
                {
                    Operation = "Equals",
                    ObjectType = "Person",
                    Path = "/Age",
                    Value = 30
                },
                new FilterPredicate
                {
                    Operation = "Equals",
                    ObjectType = "Person",
                    Path = "/City",
                    Value = "New York"
                }
            }
        };

        //Evaluate the filter for the person
        bool personResult = FilterEvaluator<Person>.EvaluateFilter(personFilter, person);
        
        // Output the result for the person
        Console.WriteLine($"Person satisfies the filter: {personResult}");
    }
}