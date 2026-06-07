using System.ComponentModel.DataAnnotations;

namespace CodeFirst.DTOs;

public class UpdateOrderRequestDto
{
    [Required]
    public int OrderId { get; set; }
}