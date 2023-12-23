using FilterObject.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class LowerThanOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthLowerThanValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "LowerThan",
            Path = "$.Warranty.DurationInMonth",
            Value = 15
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsGreaterThanValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "LowerThan",
            Path = "$.Warranty.DurationInMonth",
            Value = 10
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithDurationInMonthsEqualsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "LowerThan",
            Path = "$.Warranty.DurationInMonth",
            Value = 12
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(false);
    }
}