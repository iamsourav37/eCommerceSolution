using eCommerce.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        // DbSet Properties
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            #region Adding Roles
            // Add Roles
            Guid superAdminRoleId = Guid.Parse("367FEFF3-0FF0-4FEB-8451-16C04BDCE883");
            Guid adminRoleId = Guid.Parse("0282F81E-5691-4784-AB89-C3D0A494E8C3");
            Guid customerRoleId = Guid.Parse("6F2295D4-BCF7-447A-A1FF-941DAA524CAB");

            // Seeding roles
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid> { Id = superAdminRoleId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                new IdentityRole<Guid> { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Id = customerRoleId, Name = "Customer", NormalizedName = "CUSTOMER" }
            );
            #endregion

            #region Adding Super Admin User

            Guid superAdminId = Guid.Parse("CD2A4FEE-8AA1-4756-8D84-F878F286FDBF");
            var superAdmin = new Customer() { Id = superAdminId, Email = "superadmin@admin.com", NormalizedEmail = "SUPERADMIN@ADMIN.COM", UserName = "SuperAdmin", NormalizedUserName = "SUPERADMIN", Name = "Sourav Ganguly", SecurityStamp = superAdminId.ToString() };

            superAdmin.PasswordHash = new PasswordHasher<Customer>().HashPassword(superAdmin, "SuperAdmin@1234");
            modelBuilder.Entity<Customer>().HasData(
                superAdmin
                );
            #endregion

            // Configure Composite Key for Wishlist (CustomerId, ProductId)
            modelBuilder.Entity<Wishlist>()
                .HasKey(w => new { w.CustomerId, w.ProductId });

            // Define relationship between Order and Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Optional, based on your needs

            // Define relationship between Order and Address (ShippingAddress)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Cascade); // Optional, based on your needs

        }
    }
}
