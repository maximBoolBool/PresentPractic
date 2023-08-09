using Microsoft.EntityFrameworkCore;
using PresentPractic.Models.DbModels;

namespace PresentPractic;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Present> Presents { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PresentPracticDB;Username=postgres;Password=panzer117");
        base.OnConfiguring(optionsBuilder);
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    
}