using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Makaani.Models
{
    public partial class MakaniContext : DbContext
    {
        public MakaniContext()
        {
        }

        public MakaniContext(DbContextOptions<MakaniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<Finishes> Finishes { get; set; }
        public virtual DbSet<Follower> Follower { get; set; }
        public virtual DbSet<LastViewAds> LastViewAds { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LoveList> LoveList { get; set; }
        public virtual DbSet<LovedProductList> LovedProductList { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<PayingOffer> PayingOffer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<Regoin> Regoin { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Testimonails> Testimonails { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserSearch> UserSearch { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-7945E40\\SQLEXPRESS;Database=Makani;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ads>(entity =>
            {
                entity.Property(e => e.AdsId).HasColumnName("AdsID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Descrption)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OfferId).HasColumnName("OfferID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PromotionId).HasColumnName("PromotionID");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Ads_Category");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Ads_Department");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Ads_Location");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("FK_Ads_Offer");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Ads_Product");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_Ads_Promotion");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Ads_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.FeatureId).HasColumnName("FeatureID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Feature)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Feature_Product");
            });

            modelBuilder.Entity<Finishes>(entity =>
            {
                entity.Property(e => e.FinishesId).HasColumnName("FinishesID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.Property(e => e.FollowerId).HasColumnName("FollowerID");

                entity.Property(e => e.FirstUserId).HasColumnName("FirstUserID");

                entity.Property(e => e.SecondUserId).HasColumnName("SecondUserID");

                entity.HasOne(d => d.FirstUser)
                    .WithMany(p => p.FollowerFirstUser)
                    .HasForeignKey(d => d.FirstUserId)
                    .HasConstraintName("FK_Follower_User");

                entity.HasOne(d => d.SecondUser)
                    .WithMany(p => p.FollowerSecondUser)
                    .HasForeignKey(d => d.SecondUserId)
                    .HasConstraintName("FK_Follower_User1");
            });

            modelBuilder.Entity<LastViewAds>(entity =>
            {
                entity.Property(e => e.LastViewAdsId).HasColumnName("LastViewAdsID");

                entity.Property(e => e.AdsId).HasColumnName("AdsID");

                entity.Property(e => e.LastViewAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Ads)
                    .WithMany(p => p.LastViewAds)
                    .HasForeignKey(d => d.AdsId)
                    .HasConstraintName("FK_LastViewAds_Ads");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LastViewAds)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_LastViewAds_User");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LoactionId);

                entity.Property(e => e.LoactionId).HasColumnName("LoactionID");

                entity.Property(e => e.LoactionLabel)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MapsLink).IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProvincesId).HasColumnName("ProvincesID");

                entity.HasOne(d => d.Provinces)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.ProvincesId)
                    .HasConstraintName("FK_Location_Provinces");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastLogout).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Login_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Login_User");
            });

            modelBuilder.Entity<LoveList>(entity =>
            {
                entity.Property(e => e.LoveListId).HasColumnName("LoveListID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoveList)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoveList_User");
            });

            modelBuilder.Entity<LovedProductList>(entity =>
            {
                entity.Property(e => e.LovedProductListId).HasColumnName("LovedProductListID");

                entity.Property(e => e.AddedAt).HasColumnType("datetime");

                entity.Property(e => e.LoveListId).HasColumnName("LoveListID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.LoveList)
                    .WithMany(p => p.LovedProductList)
                    .HasForeignKey(d => d.LoveListId)
                    .HasConstraintName("FK_LovedProductList_LoveList");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LovedProductList)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_LovedProductList_Product");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.MediaId).HasColumnName("MediaID");

                entity.Property(e => e.Path).IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .HasConstraintName("FK_Media_MediaType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Image_Product");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.Property(e => e.OfferId).HasColumnName("OfferID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PayingOffer>(entity =>
            {
                entity.Property(e => e.PayingOfferId).HasColumnName("PayingOfferID");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OfferDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PayingOffer)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PayingOffer_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PayingOffer)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PayingOffer_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.FinishesId).HasColumnName("FinishesID");

                entity.HasOne(d => d.Finishes)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.FinishesId)
                    .HasConstraintName("FK_Product_Finishes");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_Product_User");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.PromotionId)
                    .HasColumnName("PromotionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.StartFrom).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Provinces>(entity =>
            {
                entity.Property(e => e.ProvincesId).HasColumnName("ProvincesID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.Property(e => e.RateId).HasColumnName("RateID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RateComment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Rate)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Rate_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rate)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Rate_User");
            });

            modelBuilder.Entity<Regoin>(entity =>
            {
                entity.Property(e => e.RegoinId).HasColumnName("RegoinID");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProvincesId).HasColumnName("ProvincesID");

                entity.HasOne(d => d.Provinces)
                    .WithMany(p => p.Regoin)
                    .HasForeignKey(d => d.ProvincesId)
                    .HasConstraintName("FK_Regoin_Provinces");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Testimonails>(entity =>
            {
                entity.Property(e => e.TestimonailsId)
                    .HasColumnName("TestimonailsID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Testimonails_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.WhatsupLink).IsUnicode(false);
            });

            modelBuilder.Entity<UserSearch>(entity =>
            {
                entity.Property(e => e.UserSearchId).HasColumnName("UserSearchID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.SearchTitle)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSearch)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserSearch_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
