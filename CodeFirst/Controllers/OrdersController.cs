using CodeFirst.DTOs;
using CodeFirst.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order is null)
        {
            return NotFound(new
            {
                message = "Order with id was not found."
            });
        }

        return Ok(order);
    }

    [HttpPut]
    public async Task<IActionResult> ProcessOrder([FromBody] UpdateOrderRequestDto request)
    {

        var result = await _orderService.ProcessOrder(request.OrderId);

        if (!result)
        {
            return BadRequest(new
            {
                message = "Order does not exist or something went wrong"
            });
        }

        return Ok(new
        {
            message = "Order processed successfully"
        });
    }
}