using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class LowerThanOrEqualOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthLowerThanValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "lte",
            Path = "$.Warranty.DurationInMonth",
            Value = 15
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsGreaterThanValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "lte",
            Path = "$.Warranty.DurationInMonth",
            Value = 10
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "lte",
            Path = "$.Warranty.DurationInMonth",
            Value = 12
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}