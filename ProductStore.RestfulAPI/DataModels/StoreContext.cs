using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProductStore.RestfulAPI.DataModels
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ExternalShipper> ExternalShippers { get; set; }
        public virtual DbSet<InternalShipper> InternalShippers { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersProduct> OrdersProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsCategory> ProductsCategories { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.;Database=Store;User Id=sa;Password=1234;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .HasColumnName("ALIAS");

                entity.Property(e => e.IdCategoryParrent).HasColumnName("ID_CATEGORY_PARRENT");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.PersonInCharge)
                    .HasMaxLength(255)
                    .HasColumnName("PERSON_IN_CHARGE");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.IdMedia).HasColumnName("ID_MEDIA");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .HasColumnName("SEX");

                entity.Property(e => e.StartedDate)
                    .HasColumnType("date")
                    .HasColumnName("STARTED_DATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<ExternalShipper>(entity =>
            {
                entity.ToTable("External_Shippers");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.IdMedia).HasColumnName("ID_MEDIA");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.PersonInCharge)
                    .HasMaxLength(255)
                    .HasColumnName("PERSON_IN_CHARGE");

                entity.Property(e => e.PersonInShipper)
                    .HasMaxLength(255)
                    .HasColumnName("PERSON_IN_SHIPPER");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");

                entity.Property(e => e.ShipperPhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("SHIPPER_PHONE_NUMBER");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("STATUS");
            });

            modelBuilder.Entity<InternalShipper>(entity =>
            {
                entity.ToTable("Internal_Shipper");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(50)
                    .HasColumnName("CMND");

                entity.Property(e => e.IdMedia).HasColumnName("ID_MEDIA");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("NAME")
                    .IsFixedLength(true);

                entity.Property(e => e.PersonInCharge)
                    .HasMaxLength(255)
                    .HasColumnName("PERSON_IN_CHARGE");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Shipper)
                    .HasMaxLength(255)
                    .HasColumnName("SHIPPER");

                entity.Property(e => e.ShipperAddress)
                    .HasMaxLength(50)
                    .HasColumnName("SHIPPER_ADDRESS");

                entity.Property(e => e.ShipperPhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("SHIPPER_PHONE_NUMBER");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("STATUS")
                    .HasComment("WORKING/LEFT OFF/LEAVING OFF");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ExternalUrl)
                    .HasMaxLength(512)
                    .HasColumnName("EXTERNAL_URL");

                entity.Property(e => e.FileId)
                    .HasMaxLength(512)
                    .HasColumnName("FILE_ID");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.Property(e => e.IdExternalShipper).HasColumnName("ID_EXTERNAL_SHIPPER");

                entity.Property(e => e.IdInternalShipper).HasColumnName("ID_INTERNAL_SHIPPER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("TYPE");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_Medias_Employees");

                entity.HasOne(d => d.IdExternalShipperNavigation)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.IdExternalShipper)
                    .HasConstraintName("FK_Medias_External_Shippers");

                entity.HasOne(d => d.IdInternalShipperNavigation)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.IdInternalShipper)
                    .HasConstraintName("FK_Medias_Internal_Shipper");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_Medias_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(255)
                    .HasColumnName("CREATE_BY");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_DATE");

                entity.Property(e => e.IdEmployee).HasColumnName("ID_EMPLOYEE");

                entity.Property(e => e.IdExternalShipper).HasColumnName("ID_EXTERNAL_SHIPPER");

                entity.Property(e => e.IdInternalShipper).HasColumnName("ID_INTERNAL_SHIPPER");

                entity.Property(e => e.IdPaymentMethod).HasColumnName("ID_PAYMENT_METHOD");

                entity.Property(e => e.OrderCode)
                    .HasMaxLength(255)
                    .HasColumnName("ORDER_CODE");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.IdExternalShipperNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdExternalShipper)
                    .HasConstraintName("FK_Orders_External_Shippers");

                entity.HasOne(d => d.IdInternalShipperNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdInternalShipper)
                    .HasConstraintName("FK_Orders_Internal_Shipper");
            });

            modelBuilder.Entity<OrdersProduct>(entity =>
            {
                entity.HasKey(e => new { e.IdOrder, e.IdProduct })
                    .HasName("PK_Orders_Products_1");

                entity.ToTable("Orders_Products");

                entity.Property(e => e.IdOrder).HasColumnName("ID_ORDER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrdersProducts)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Products_Orders");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OrdersProducts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Products_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Height).HasColumnName("HEIGHT");

                entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");

                entity.Property(e => e.IdCompany).HasColumnName("ID_COMPANY");

                entity.Property(e => e.IdMaterials)
                    .HasMaxLength(10)
                    .HasColumnName("ID_MATERIALS")
                    .IsFixedLength(true);

                entity.Property(e => e.IdMedia).HasColumnName("ID_MEDIA");

                entity.Property(e => e.IdTransporter).HasColumnName("ID_TRANSPORTER");

                entity.Property(e => e.Lenght).HasColumnName("LENGHT");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .HasColumnName("NAME")
                    .IsFixedLength(true);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.Property(e => e.Weight).HasColumnName("WEIGHT");

                entity.Property(e => e.Width).HasColumnName("WIDTH");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Products_Categories");

                entity.HasOne(d => d.IdCompanyNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCompany)
                    .HasConstraintName("FK_Products_Companys");
            });

            modelBuilder.Entity<ProductsCategory>(entity =>
            {
                entity.ToTable("Products_Categories");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("ALIAS");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
