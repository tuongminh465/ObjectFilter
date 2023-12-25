namespace Qilin.Core.QilinShared.Common.Constants;

public class FilterPredicateOperator
{
    #region LogicalOperators
    
    public const string And = "and";
    public const string Or = "or";
    public const string Not = "not";
    
    #endregion
    
    #region GenericOperators

    public const string Equals = "equals";
    public const string NotEqual = "notequal";
    public const string Null = "null";
    public const string NotNull = "notnull";

    #endregion

    #region StringOperators

    public const string Empty = "empty";
    public const string NotEmpty = "notempty";
    public const string Contains = "contains";

    #endregion

    #region NumberOperators

    public const string GreaterThan = "gt";
    public const string GreaterThanOrEqual = "gte";
    public const string LowerThan = "lt";
    public const string LowerThanOrEqual = "lte";

    #endregion

    #region ArrayOperators

    public const string ArrayEmpty = "arrayempty";
    public const string ArrayNotEmpty = "arraynotempty";
    public const string ArrayContains = "arraycontains";

    #endregion
}