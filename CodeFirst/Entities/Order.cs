namespace CodeFirst.Entities;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public String Status { get; set; }
    public decimal TotalAmount { get; set; }
    
    public int Users_UserId { get; set; }
    
    public User User { get; set; }
    
    public ICollection<Payment> Payments { get; set; } = [];
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    
}