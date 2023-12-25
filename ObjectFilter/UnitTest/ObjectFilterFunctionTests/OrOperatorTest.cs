using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class OrOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void OrOperationTest_WithOneOutOfTwoConditionsCorrect_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Or",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "lt",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                },
                new()
                {
                    Operation = "gt",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 10
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdContainsValueOrDurationInMonthNotEqualsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "Or",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-45" 
                },
                new()
                {
                    Operation = "Equals",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 10
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithThreeFilterConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Or",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "ArrayContains",
                    Path = "$.VariationIds",
                    Value = "ext-var-1"
                },
                new()
                {
                    Operation = "ArrayContains",
                    Path = "$.VariationIds",
                    Value = "ext-var-3"
                },
                new()
                {
                    Operation = "ArrayContains",
                    Path = "$.VariationIds",
                    Value = "ext-var-4"
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}