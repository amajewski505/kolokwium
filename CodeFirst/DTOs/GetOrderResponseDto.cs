namespace CodeFirst.DTOs;

public class GetOrderResponseDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public decimal TotalAmount { get; set; }

    public UserDto User { get; set; } = null!;
    public List<OrderItemDto> OrderItems { get; set; } = new();
    public List<PaymentDto> Payments { get; set; } = new();
}

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class PaymentDto
{
    public int PaymentId { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public decimal Amount { get; set; }
    public string PaymentStatus { get; set; } = null!;
}