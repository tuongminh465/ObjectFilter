using ObjectFilter.Model;

namespace UnitTest.ObjectFilterFunctionTests;

public class ObjectFilterFunctionTestBase
{
    protected Product _product;

    [SetUp]
    public void Setup()
    {
        // Make object simple to easy follow
        _product = new Product
        {
            BrandId = "ext-brand-23",
            VariationIds = new List<string> { "ext-var-1", "ext-var-2" },
            Warranty = new Warranty
            {
                DurationInMonth = 12,
                WarrantyType = null
            }
        };
    }
}