using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class AndOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdContainsValueAndDurationInMonthEqualsValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "And",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-23" 
                },
                new()
                {
                    Operation = "Equals",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 12
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithBrandIdContainsValueAndDurationInMonthNotEqualsValue_ShouldReturnFalse()
    {
        var filter = new FilterPredicate
        {
            Operation = "And",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-23" 
                },
                new()
                {
                    Operation = "Equals",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
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
            Operation = "And",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Equals",
                    Path = "$.BrandId",
                    Value = "ext-brand-23" 
                },
                new()
                {
                    Operation = "lt",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                },
                new()
                {
                    Operation = "ArrayContains",
                    Path = "$.VariationIds",
                    Value = "ext-var-2"
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}