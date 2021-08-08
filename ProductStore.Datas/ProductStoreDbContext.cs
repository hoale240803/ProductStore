
using ProductStore.Models.Models;
using System.Data.Entity;

namespace ProductStore.Model
{
    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext() : base("ProductStoreConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Categories> Categories { set; get; }
        public DbSet<Orders> Orders { set; get; }
        public DbSet<OrderDetails> OrderDetails { set; get; }
        public DbSet<PostCategories> PostCategories { set; get; }
        public DbSet<Posts> Posts { set; get; }
        public DbSet<PostTags> PostTags { set; get; }
        public DbSet<ProductCategories> ProductCategories { set; get; }
        public virtual DbSet<Products> Products { get; set; }
        public DbSet<ProductTags> ProductTags { set; get; }
        public DbSet<Tags> Tags { set; get; }
        public DbSet<VisitorStatistics> VisitorStatistics { set; get; }


        public static ProductStoreDbContext Create()
        {
            return new ProductStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
         
        }
    }
}