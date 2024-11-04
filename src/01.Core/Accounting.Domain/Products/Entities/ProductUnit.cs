namespace Accounting.Domain.Products.Entities;

public class ProductUnit
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UnitId { get; set; }
}