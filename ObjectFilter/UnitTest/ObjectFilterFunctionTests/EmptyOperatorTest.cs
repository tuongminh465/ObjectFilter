using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class EmptyOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdEmpty_ShouldReturnTrue()
    {
        _product.BrandId = "";
        
        var filter = new FilterPredicate
        {
            Operation = "Empty",
            Path = "$.BrandId"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdNotEmpty_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Empty",
            Path = "$.BrandId"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithWarrantyTypeNull_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Empty",
            Path = "$.Warranty.WarrantyType"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationIdsEmpty_ShouldReturnTrue()
    {
        _product.VariationIds = new List<string>();
        
        var filter = new FilterPredicate
        {
            Operation = "Empty",
            Path = "$.VariationIds"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationIdsNotEmpty_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Empty",
            Path = "$.VariationIds"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
}