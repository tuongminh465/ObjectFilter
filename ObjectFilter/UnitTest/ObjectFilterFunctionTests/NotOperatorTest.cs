using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class NotOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdNotContainsValueOrDurationInMonthLowerThanValue_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Not",
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
                    Operation = "gt",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
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
            Operation = "Not",
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

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithThreeFilterConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Not",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Null",
                    Path = "$.BrandId",
                },
                new()
                {
                    Operation = "gte",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                },
                new()
                {
                    Operation = "ArrayEmpty",
                    Path = "$.VariationIds",
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}