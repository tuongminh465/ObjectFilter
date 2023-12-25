using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class NotEmptyOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdNotEmpty_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "NotEmpty",
            Path = "$.BrandId"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdmpty_ShouldReturnFalse()
    {
        _product.BrandId = "";
        
        var filter = new FilterPredicate
        {
            Operation = "NotEmpty",
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
            Operation = "NotEmpty",
            Path = "$.Warranty.WarrantyType"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationIdsEmpty_ShouldReturnFalse()
    {
        _product.VariationIds = new List<string>();
        
        var filter = new FilterPredicate
        {
            Operation = "ArrayNotEmpty",
            Path = "$.VariationIds"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationIdsNotEmpty_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "ArrayNotEmpty",
            Path = "$.VariationIds"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}