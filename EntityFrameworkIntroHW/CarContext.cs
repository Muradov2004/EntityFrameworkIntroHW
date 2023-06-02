using Microsoft.EntityFrameworkCore;

public class CarContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public CarContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Cars;Integrated Security=True;"); 
    }
}