using Microsoft.EntityFrameworkCore;
using Product_MVC.Models.Domains;

namespace Product_MVC.Models.Services
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        //Property of DBSet Ables to Create a Table According to Name
        public DbSet<Product> Products { get; set; }

    }

    
}
