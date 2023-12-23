using ObjectFilter.Model;

namespace UnitTest;

public class ObjectFilterFunctionTestBase
{
    protected Product _product;

    [SetUp]
    public void Setup()
    {
        _product = new Product
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
    }
}