using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SneakerShopAPI.Models
{
    public partial class SneakerShopContext : DbContext
    {
        public SneakerShopContext()
        {
        }

        public SneakerShopContext(DbContextOptions<SneakerShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<PhotoProduct> PhotoProduct { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddress { get; set; }
        public virtual DbSet<WishList> WishList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__F3DBC57395F98C90");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DelFlg)
                    .HasColumnName("delFlg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(200);

                entity.Property(e => e.InsBy)
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandId)
                    .HasColumnName("brandId")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BrandNm)
                    .IsRequired()
                    .HasColumnName("brandNm")
                    .HasMaxLength(200);

                entity.Property(e => e.DelFlg).HasColumnName("delFlg");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasColumnName("photo")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .HasColumnName("orderId")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasColumnName("phonenumber")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverNm)
                    .IsRequired()
                    .HasColumnName("receiverNm")
                    .HasMaxLength(200);

                entity.Property(e => e.ShippingAddress)
                    .IsRequired()
                    .HasColumnName("shippingAddress")
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnName("orderId")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasColumnName("product");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<PhotoProduct>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlg).HasColumnName("delFlg");

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PhotoProduct)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Photo_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BrandId)
                    .HasColumnName("brandId")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DelFlg).HasColumnName("delFlg");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductNm)
                    .IsRequired()
                    .HasColumnName("productNm")
                    .HasMaxLength(200);

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Product_Brand");
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(500);

                entity.Property(e => e.DelFlg).HasColumnName("delFlg");

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDefault).HasColumnName("isDefault");

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.ShippingAddress)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingAddress_User");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DelFlg).HasColumnName("delFlg");

                entity.Property(e => e.InsBy)
                    .IsRequired()
                    .HasColumnName("insBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsDatetime)
                    .HasColumnName("insDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("productId")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UpdBy)
                    .IsRequired()
                    .HasColumnName("updBy")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDatetime)
                    .HasColumnName("updDatetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishList)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishList_Product");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.WishList)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishList_Users");
            });
        }
    }
}
