using CodeFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data;

public class AppDbContext: DbContext
{
    protected AppDbContext() {}
    
    public AppDbContext(DbContextOptions options) : base(options)
    {}
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>(e =>
    {
        e.HasKey(k => k.UserId);

        e.Property(user => user.Username).HasMaxLength(100);
        e.Property(user => user.Email).HasMaxLength(100);
        e.Property(user => user.PasswordHash).HasMaxLength(100);
        e.Property(user => user.CreatedAt).HasColumnType("date");

        e.ToTable("Users");
    });

    modelBuilder.Entity<Product>(e =>
    {
        e.HasKey(k => k.ProductId);

        e.Property(product => product.Name).HasMaxLength(100);
        e.Property(product => product.Description).HasMaxLength(100);
        e.Property(product => product.Price).HasColumnType("decimal(10,2)");
        e.Property(product => product.StockQuantity);

        e.ToTable("Products");
    });

    modelBuilder.Entity<OrderItem>(e =>
    {
        e.HasKey(k => new { k.OrderId, k.ProductId });
        e.Property(orderItem => orderItem.Quantity);
        e.Property(orderItem => orderItem.Price).HasColumnType("decimal(10,2)");

        
        e.HasOne<Order>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
        
        e.HasOne<Product>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        e.ToTable("Order_Items");
    });

    modelBuilder.Entity<Order>(e =>
    {
        e.HasKey(order => order.OrderId);

        e.Property(order => order.OrderDate).HasColumnType("date");

        e.Property(order => order.Status).HasMaxLength(100).IsRequired();

        e.Property(order => order.TotalAmount).HasColumnType("decimal(10,2)");

        e.Property(order => order.Users_UserId).IsRequired();

        e.ToTable("Orders");

        e.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.Users_UserId)
            .OnDelete(DeleteBehavior.NoAction);
    });

    modelBuilder.Entity<Payment>(e =>
    {
        e.HasKey(k => k.PaymentId);

        e.Property(payment => payment.PaymentMethod).HasMaxLength(100);
        e.Property(payment => payment.Amount).HasColumnType("decimal(10,2)");

        e.Property(payment => payment.PaymentStatus).HasMaxLength(100);
        e.Property(payment => payment.Orders_OrderId);

        e.ToTable("Payments");
        
        e.HasOne<Order>()
            .WithMany()
            .HasForeignKey(payment => payment.Orders_OrderId)
            .OnDelete(DeleteBehavior.NoAction);
    });

    
modelBuilder.Entity<User>().HasData(
    new User
    {
        UserId = 1,
        Username = "example",
        Email = "example@example.com",
        PasswordHash = "example",
        CreatedAt = new DateTime(2024, 1, 10)
    },
    new User
    {
        UserId = 2,
        Username = "example",
        Email = "example@example.com",
        PasswordHash = "example",
        CreatedAt = new DateTime(2024, 2, 15)
    }
);

modelBuilder.Entity<Product>().HasData(
    new Product
    {
        ProductId = 1,
        Name = "example",
        Description = "example laptop",
        Price = 4500.00m,
        StockQuantity = 10
    },
    new Product
    {
        ProductId = 2,
        Name = "example",
        Description = "example mouse",
        Price = 120.00m,
        StockQuantity = 50
    },
    new Product
    {
        ProductId = 3,
        Name = "Keyboard",
        Description = "example keyboard",
        Price = 350.00m,
        StockQuantity = 25
    },
    new Product
    {
        ProductId = 4,
        Name = "Monitor",
        Description = "example monitor",
        Price = 900.00m,
        StockQuantity = 15
    }
);

modelBuilder.Entity<Order>().HasData(
    new Order
    {
        OrderId = 1,
        OrderDate = new DateTime(2024, 3, 1),
        Status = "New",
        TotalAmount = 4740.00m,
        Users_UserId = 1
    },
    new Order
    {
        OrderId = 2,
        OrderDate = new DateTime(2024, 3, 5),
        Status = "Pending",
        TotalAmount = 1250.00m,
        Users_UserId = 2
    },
    new Order
    {
        OrderId = 3,
        OrderDate = new DateTime(2024, 3, 10),
        Status = "New",
        TotalAmount = 700.00m,
        Users_UserId = 1
    }
);

modelBuilder.Entity<OrderItem>().HasData(
    new OrderItem
    {
        OrderId = 1,
        ProductId = 1,
        Quantity = 1,
        Price = 4500.00m
    },
    new OrderItem
    {
        OrderId = 1,
        ProductId = 2,
        Quantity = 2,
        Price = 120.00m
    },
    new OrderItem
    {
        OrderId = 2,
        ProductId = 4,
        Quantity = 1,
        Price = 900.00m
    },
    new OrderItem
    {
        OrderId = 2,
        ProductId = 3,
        Quantity = 1,
        Price = 350.00m
    },
    new OrderItem
    {
        OrderId = 3,
        ProductId = 3,
        Quantity = 2,
        Price = 350.00m
    }
);

modelBuilder.Entity<Payment>().HasData(
    new Payment
    {
        PaymentId = 1,
        PaymentMethod = "Card",
        Amount = 4740.00m,
        PaymentStatus = "Completed",
        Orders_OrderId = 1
    },
    new Payment
    {
        PaymentId = 2,
        PaymentMethod = "Transfer",
        Amount = 1250.00m,
        PaymentStatus = "Pending",
        Orders_OrderId = 2
    }
);
    

    base.OnModelCreating(modelBuilder);
}
}










