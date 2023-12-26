using ObjectFilter.Functions;
using ObjectFilter.Model;
using Shouldly;

namespace UnitTest.ObjectFilterFunctionTests;

public class NullOperatorTest : ObjectFilterFunctionTestBase
{
    [Test]
    public void NullOperatorTest_WithWarrantTypeNull_ShouldReturnTrue()
    {
        var filter = new FilterPredicate
        {
            Operation = "Null",
            Path = "$.Warranty.WarrantyType"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(true);
    }
    
    [Test]
    public void NullOperatorTest_WithWarrantTypeNotNull_ShouldReturnFalse()
    {
        _product.Warranty.WarrantyType = "Normal";
        
        var filter = new FilterPredicate
        {
            Operation = "Null",
            Path = "$.Warranty.WarrantyType"
        };

        var result = ObjectEvaluator.EvaluateObject(filter, _product);

        result.ShouldBe(false);
    }
}