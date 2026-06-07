namespace CodeFirst.Entities;

public class User
{
    public int UserId { get; set; }
    public String Username { get; set; }
    public String Email { get; set; }
    public String PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Order> Orders { get; set; } = [];
}