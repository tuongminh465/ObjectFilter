using FilterObject.Functions;
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
                    Operation = "GreaterThan",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                }
            }
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

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

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

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
                    Operation = "GreaterThanOrEqual",
                    Path = "$.Warranty.DurationInMonth",
                    Value = 15
                },
                new()
                {
                    Operation = "Empty",
                    Path = "$.VariationIds",
                }
            }
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithNestedAndConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Not",
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
                            Value = 10
                        },
                        new()
                        {
                            Operation = "Contains",
                            Path = "$.VariationIds",
                            Value = "ext-var-2"
                        }
                    }
                }
            }
        };

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void ObjectFilterFunction_WithNestedOrConditions_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Not",
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
                    Operation = "Or",
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

        var result = ObjectFilterFunction.EvaluateFilter(filter, _product);

        result.ShouldBe(true);
    }
}