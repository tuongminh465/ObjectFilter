using System.Reflection;

namespace ObjectFilter.Model;

public class FilterPredicate
{
    public string Operation { get; set; }
    public string ObjectType { get; set; }
    public string Path { get; set; }
    public object Value { get; set; }
    public List<FilterPredicate> Apply { get; set; }
}