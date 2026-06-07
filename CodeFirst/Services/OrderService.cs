using CodeFirst.Data;
using CodeFirst.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetOrderResponseDto?> GetOrderById(int id)
    {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.Payments)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order is null)
        {
            return null;
        }

        return new GetOrderResponseDto
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,

            User = new UserDto
            {
                UserId = order.User.UserId,
                Username = order.User.Username,
                Email = order.User.Email
            },

            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList(),

            Payments = order.Payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                PaymentMethod = p.PaymentMethod,
                Amount = p.Amount,
                PaymentStatus = p.PaymentStatus
            }).ToList()
        };
    }

    public async Task<bool> ProcessOrder(int orderId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Payments)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order is null)
        {
            return false;
        }

        if (order.Payments.Any())
        {
            return false;
        }

        order.Status = "Processed";

        foreach (var item in order.OrderItems)
        {
            item.Price *= 0.9m;
        }

        order.TotalAmount = order.OrderItems
            .Sum(item => item.Price * item.Quantity);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }
}