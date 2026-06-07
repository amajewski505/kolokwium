namespace CodeFirst.Entities;

public class Product
{
    public int ProductId { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}