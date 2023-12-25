using FilterObject.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class EqualsOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.BrandId",
            Value = "ext-brand-23"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdNotEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.BrandId",
            Value = "ext-brand-45"
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.Warranty.DurationInMonth",
            Value = 12
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsNotEqualsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.Warranty.DurationInMonth",
            Value = 10
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
}