using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class EqualsOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void EqualsOperatorTest_WithBrandIdEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.BrandId",
            Value = "ext-brand-23"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }

    [Test]
    public void EqualsOperatorTest_WithBrandIdNotEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Equals",
            Path = "$.BrandId",
            Value = "ext-brand-45"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
}