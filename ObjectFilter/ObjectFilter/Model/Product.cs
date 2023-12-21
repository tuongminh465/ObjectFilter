namespace ObjectFilter.Model;

public class Product
{
    public string? BrandId { get; set; }
    public List<string> VariationIds { get; set; }
    public string Color { get; set; }
    public Warranty Warranty { get; set; }
}