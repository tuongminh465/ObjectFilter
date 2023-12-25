using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class OrOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void ObjectFilterFunction_WithBrandIdContainsValueOrDurationInMonthGreaterThanValue_ShouldReturnTrue()
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
                    Operation = "GreaterThan",
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
                    Operation = "Equals",
                    Path = "$.BrandId",
                    Value = "ext-brand-45" 
                },
                new()
                {
                    Operation = "GreaterThanOrEqual",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                },
                new()
                {
                    Operation = "Contains",
                    Path = "$.VariationIds",
                    Value = "ext-var-1"
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithNestedAndConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Or",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Equals",
                    Path = "$.BrandId",
                    Value = "ext-brand-45" 
                },
                new()
                {
                    Operation = "And",
                    Apply = new List<FilterPredicate>
                    {
                        new()
                        {
                            Operation = "LowerThanOrEqual",
                            Path = "$.Warranty.DurationInMonth",
                            Value = 15
                        },
                        new()
                        {
                            Operation = "Contains",
                            Path = "$.VariationIds",
                            Value = "ext-var-1"
                        }
                    }
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithNestedNotConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Or",
            Apply = new List<FilterPredicate>
            {
                new()
                {
                    Operation = "Equals",
                    Path = "$.BrandId",
                    Value = "ext-brand-45" 
                },
                new()
                {
                    Operation = "Not",
                    Apply = new List<FilterPredicate>
                    {
                        new()
                        {
                            Operation = "Null",
                            Path = "$.Warranty.DurationInMonth",
                        },
                        new()
                        {
                            Operation = "Empty",
                            Path = "$.VariationIds",
                        }
                    }
                }
            }
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
}