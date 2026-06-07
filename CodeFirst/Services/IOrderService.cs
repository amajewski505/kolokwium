using CodeFirst.DTOs;

namespace CodeFirst.Services;

public interface IOrderService
{
    Task<GetOrderResponseDto?> GetOrderById(int id);
    Task<bool> ProcessOrder(int orderId);
}