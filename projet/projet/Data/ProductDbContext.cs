using Microsoft.EntityFrameworkCore;

//Creating a session instance to connect to the database using DbContext instance
//Note: We already installed the adequat dependecies to our project 
namespace projet.Data
{
    public class ProductDbContext : DbContext
    {
        //the configuration builderfor the database provider is indicated in Program.cs file
        //inialize a new instance of DbContext with options
        public ProductDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
