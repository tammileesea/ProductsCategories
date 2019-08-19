using Microsoft.EntityFrameworkCore;

namespace prodCat.Models {
    public class productCatContext : DbContext {
        public productCatContext(DbContextOptions options) : base(options) {}
        public DbSet<Product> products {get;set;}
        public DbSet<Category> categories {get;set;}
        public DbSet<Association> associations {get;set;}
    }
}