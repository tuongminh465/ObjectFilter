using System.Reflection;

namespace ObjectFilter.Model;

public class FilterPredicate
{
    // create operation constants
    public string Operation { get; set; }
    public string? Path { get; set; }
    public object? Value { get; set; }
    public List<FilterPredicate>? Apply { get; set; }
}