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
        modelBuilder.Entity<OrderRow>()
        .HasKey(or => new {or.OrderId, or.ProductId});
        modelBuilder.Entity<OrderRow>()
        .HasOne(or => or.Product)
        .WithMany(p => p.OrderRows)
        .HasForeignKey(or => or.ProductId);
        modelBuilder.Entity<OrderRow>()
        .HasOne(or => or.Order)
        .WithMany(o => o.OrderRows)
        .HasForeignKey(or => or.OrderId);
    }
}