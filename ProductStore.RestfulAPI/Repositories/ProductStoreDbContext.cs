using Microsoft.EntityFrameworkCore;
using ProductStore.RestfulAPI.Data;
using ProductStore.RestfulAPI.Data.Products;

namespace ProductStore.RestfulAPI.Repositories
{
    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext(DbContextOptions<ProductStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Orders> Orders { set; get; }
        public DbSet<ProductsEntity> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { set; get; }
        public DbSet<PostCategories> PostCategories { set; get; }
        public DbSet<Posts> Posts { set; get; }
        public DbSet<PostTagsEntity> PostTags { set; get; }
        public DbSet<ProductCategoryEntity> ProductCategories { set; get; }
        public DbSet<ProductTagsEntity> ProductTags { set; get; }
        public DbSet<TagsEntity> Tags { set; get; }
        public DbSet<VisitorStatistics> VisitorStatistics { set; get; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderDetails>().HasKey(table => new
            {
                table.OrderID,
                table.ProductID
            });
            builder.Entity<PostTagsEntity>().HasKey(table => new
            {
                table.PostID,
                table.TagID
            });
            builder.Entity<ProductTagsEntity>().HasKey(table => new
            {
                table.ProductID,
                table.TagID
            });
        }
    }
}