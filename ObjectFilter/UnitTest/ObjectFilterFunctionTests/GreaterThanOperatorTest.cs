using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class GreaterThanOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void GreaterThanOperatorTest_WithDurationInMonthsGreaterThanValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "gt",
            Path = "$.Warranty.DurationInMonth",
            Value = 10
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void GreaterThanOperatorTest_WithDurationInMonthsLowerThanValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "gt",
            Path = "$.Warranty.DurationInMonth",
            Value = 15
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void GreaterThanOperatorTest_WithDurationInMonthsEqualsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "gt",
            Path = "$.Warranty.DurationInMonth",
            Value = 12
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
}