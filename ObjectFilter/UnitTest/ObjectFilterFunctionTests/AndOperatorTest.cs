using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class AndOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void AndOperatorTest_WithTwoConditionsAreTrue_ShouldReturnTrue()
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
    public void AndOperatorTest_WithTwoConditionsButOneIsFalse_ShouldReturnFalse()
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
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-45" 
                },
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void AndOperatorTest_WithTwoConditionsAreFalse_ShouldReturnFalse()
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
                    Value = "brand-45" 
                },
                new()
                {
                    Operation = "Contains",
                    Path = "$.BrandId",
                    Value = "brand-67" 
                },
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
    
    [Test]
    public void ObjectFilterFunction_WithThreeConditionsAreTrue_ShouldReturnTrue()
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