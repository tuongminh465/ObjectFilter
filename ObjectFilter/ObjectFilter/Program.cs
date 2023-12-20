using ObjectFilter.Helper;
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
            Color = "red",
            Warranty = new Warranty {
                DurationInMonths = 12,
                WarrantyType = "Normal"
            }
        };

        var value = ObjectFilterHelper.GetPropertyValue(product, "/warranty/durationinmonths");
    }
}