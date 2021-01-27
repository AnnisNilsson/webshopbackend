using Microsoft.EntityFrameworkCore;

public class ProductContext : DbContext{
    //detta är vår constructor
    public ProductContext(DbContextOptions<ProductContext> options) 
    : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=DB/products.db");
    }

    public DbSet<Product> Products{get; set;}
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderRow> OrderRows { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OrderRow>()
        .HasKey(or => new { or.OrderId, or.ProductId });
        modelBuilder.Entity<OrderRow>()
        //.HasForeignKey(p => p.ProductId)
        .HasOne(or => or.Product)
        .WithMany(p => p.OrderRows);
        modelBuilder.Entity<OrderRow>()
        .HasOne(or => or.Order)
        .WithMany(o => o.OrderRows);
        //.HasForeignKey(o => o.OrderId);
        modelBuilder.Entity<OrderRow>()
        .HasKey(or => new { or.OrderId, or.ProductId });
        modelBuilder.Entity<Order>()
        .HasOne(o=> o.Customer)
        .WithMany(c=> c.Orders);
        //customer
        Customer c = new Customer {Id = 12, Name = "Kund", Adress = "Coola vägen", City = "Västerås" };
        modelBuilder.Entity<Customer>().HasData(c);

        Customer c2 = new Customer {Id = 1, Name = "Klas", Adress = "En vägen", City = "Borås" };
        modelBuilder.Entity<Customer>().HasData(c2);
        //product
        Product p = new Product {Id = 1, Price = 400, Title = "Skogs Träd", Description = "Ett träd i skogen", Image = "https://images.pexels.com/photos/142497/pexels-photo-142497.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p);

        Product p2 = new Product {Id = 2, Price = 500, Title = "Dim Träd", Description = "Ett träd i en dimmig skogen", Image = "https://images.pexels.com/photos/173388/pexels-photo-173388.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p2);

        Product p3 = new Product {Id = 3, Price = 650, Title = "Sol Träd", Description = "Ett träd i en solig skogen", Image = "https://images.pexels.com/photos/1563604/pexels-photo-1563604.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p3);

        Product p4 = new Product {Id = 4, Price = 440, Title = "Raka Träd", Description = "Ett träd i en rak skogen", Image = "https://images.pexels.com/photos/1414535/pexels-photo-1414535.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p4);

        Product p5 = new Product {Id = 5, Price = 550, Title = "Röda Träd", Description = "Ett träd rött träd", Image = "https://images.pexels.com/photos/1547813/pexels-photo-1547813.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p5);

        Product p6 = new Product {Id = 6, Price = 250, Title = "Väg Träd", Description = "Ett väg träd", Image = "https://images.pexels.com/photos/39811/pexels-photo-39811.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500"};
        modelBuilder.Entity<Product>().HasData(p6);

//order
         modelBuilder.Entity<Order>().HasData(new Order{ Id = 1, CustomerId = c.Id, TotalPrice = 600});
         Order o = new Order {Id = 2, CustomerId = c.Id, TotalPrice = 600};
         modelBuilder.Entity<Order>().HasData(o);

 //orderrows
        modelBuilder.Entity<OrderRow>().HasData(new OrderRow{OrderId = o.Id, ProductId = p.Id});
        
        modelBuilder.Entity<OrderRow>().HasData(new OrderRow{OrderId = o.Id, ProductId = p2.Id});



    }
}