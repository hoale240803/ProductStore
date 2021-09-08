using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductStore.API.DBFirst.Authentication;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class StoreContext : IdentityDbContext<StoreUser>
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ExportProductToExcel> ExportProductToExcels { get; set; }
        public virtual DbSet<ExternalShipper> ExternalShippers { get; set; }
        public virtual DbSet<InternalShipper> InternalShippers { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersProduct> OrdersProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsCategory> ProductsCategories { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

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

            //modelBuilder.Entity<AspNetRoles>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUsers>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogins>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRole>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserTokens>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("ALIAS");

                entity.Property(e => e.IdCategoryParrent).HasColumnName("ID_CATEGORY_PARRENT");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.Property(e => e.Id).HasColumnName("ID");

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

            modelBuilder.Entity<ExportProductToExcel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EXPORT_PRODUCT_TO_EXCEL");

                entity.Property(e => e.Category)
                    .HasMaxLength(500)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Company)
                    .HasMaxLength(255)
                    .HasColumnName("COMPANY");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Height).HasColumnName("HEIGHT");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Lenght).HasColumnName("LENGHT");

                entity.Property(e => e.Material)
                    .HasMaxLength(500)
                    .HasColumnName("MATERIAL");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.Property(e => e.Transporter)
                    .HasMaxLength(500)
                    .HasColumnName("TRANSPORTER");

                entity.Property(e => e.Weight).HasColumnName("WEIGHT");

                entity.Property(e => e.Width).HasColumnName("WIDTH");
            });

            modelBuilder.Entity<ExternalShipper>(entity =>
            {
                entity.ToTable("External_Shippers");

                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.Property(e => e.Id).HasColumnName("ID");

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
                entity.Property(e => e.Id).HasColumnName("ID");

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
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

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
                    .IsFixedLength(true)
                    .HasComment("ORDERING, ORDERED, PACKAGING, READY TO SHIP, SHIPPING, SHIPPED,");

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

                entity.Property(e => e.IdOrder)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_ORDER");

                entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrdersProducts)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Products_Orders");

                //entity.HasOne(d => d.IdProductNavigation)
                //    .WithMany(p => p.OrdersProducts)
                //    .HasForeignKey(d => d.IdProduct)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Orders_Products_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("ID");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Height)
                    .HasColumnName("HEIGHT")
                    .HasComment("unit (cm)");

                entity.Property(e => e.IdCategory).HasColumnName("ID_CATEGORY");

                entity.Property(e => e.IdCompany).HasColumnName("ID_COMPANY");

                entity.Property(e => e.IdMaterials)
                    .HasMaxLength(10)
                    .HasColumnName("ID_MATERIALS")
                    .IsFixedLength(true);

                entity.Property(e => e.IdTransporter).HasColumnName("ID_TRANSPORTER");

                entity.Property(e => e.Lenght)
                    .HasColumnName("LENGHT")
                    .HasComment("unit (cm)");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("PRICE")
                    .HasComment("<100,000,000");

                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true)
                    .HasComment("AVAILABLE, VERIFYING, REPORTING, SOLD OUT, ACTIVE, INACTIVE");

                entity.Property(e => e.Stock)
                    .HasColumnName("STOCK")
                    .HasComment("<100,000");

                entity.Property(e => e.Weight)
                    .HasColumnName("WEIGHT")
                    .HasComment("unit (gram)");

                entity.Property(e => e.Width)
                    .HasColumnName("WIDTH")
                    .HasComment("unit (cm)");

                //entity.HasOne(d => d.IdCategoryNavigation)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.IdCategory)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Products_Categories");

                //entity.HasOne(d => d.IdCompanyNavigation)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.IdCompany)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Products_Companys");
            });

            modelBuilder.Entity<ProductsCategory>(entity =>
            {
                entity.ToTable("Products_Categories");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("ALIAS");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("NAME");
            });


            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
