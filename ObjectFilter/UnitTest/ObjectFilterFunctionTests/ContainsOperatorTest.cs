using FilterObject.Functions;
using ObjectFilter.Model;
using Shouldly;
using UnitTest.ObjectFilterFunctionTests;

namespace UnitTest;

public class ContainsOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdContainsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.BrandId",
            Value = "brand-23"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdNotContainsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.BrandId",
            Value = "brand-45"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationsIdContainsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.VariationIds",
            Value = "ext-var-2"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithCaseSensitiveBrandIdContainsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.BrandId",
            Value = "Brand-23"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithVariationsIdsNotContainsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.VariationIds",
            Value = "ext-var-3"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithCaseSensitiveVariationsIdContainsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Contains",
            Path = "$.VariationIds",
            Value = "Ext-Var-2"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
}