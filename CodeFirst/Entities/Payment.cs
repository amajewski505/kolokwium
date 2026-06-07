namespace CodeFirst.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public String PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public String PaymentStatus { get; set; }
    public int Orders_OrderId { get; set; }
    
    public Order Order { get; set; } = null!;
    
}