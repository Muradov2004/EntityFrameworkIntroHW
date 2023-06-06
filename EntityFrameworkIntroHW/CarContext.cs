using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

public class CarContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public CarContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connecionString;
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("AppConfig.json");
        var config = builder.Build();
        connecionString = config.GetConnectionString("DefaultConnection")!;
        optionsBuilder.UseSqlServer(connecionString); 
    }
}