using Microsoft.EntityFrameworkCore;
using BTL_Ninh_Kho.Modules.Warehouse.Models;

namespace BTL_Ninh_Kho.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BTL_Ninh_Kho.Modules.Warehouse.Models.Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ImportRequest> ImportRequests { get; set; }
        public DbSet<ImportRequestDetail> ImportRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho Employee
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId);

            // Cấu hình cho Warehouse
            modelBuilder.Entity<BTL_Ninh_Kho.Modules.Warehouse.Models.Warehouse>()
                .HasOne(w => w.Manager)
                .WithMany()
                .HasForeignKey(w => w.ManagerId);

            // Cấu hình cho ImportRequest
            modelBuilder.Entity<ImportRequest>()
                .ToTable("tblDonnhap")
                .Property(i => i.ID).HasColumnName("ID");

            modelBuilder.Entity<ImportRequest>()
                .Property(i => i.SupplierId).HasColumnName("iNCC");

            modelBuilder.Entity<ImportRequest>()
                .Property(i => i.WarehouseId).HasColumnName("iKho");

            modelBuilder.Entity<ImportRequest>()
                .Property(i => i.ImportDate).HasColumnName("dNgaynhap");

            modelBuilder.Entity<ImportRequest>()
                .Property(i => i.Status).HasColumnName("iTrangthai");

            // Cấu hình quan hệ cho ImportRequest
            modelBuilder.Entity<ImportRequest>()
                .HasOne(i => i.Supplier)
                .WithMany()
                .HasForeignKey(i => i.SupplierId);

            modelBuilder.Entity<ImportRequest>()
                .HasOne(i => i.Warehouse)
                .WithMany()
                .HasForeignKey(i => i.WarehouseId);

            // Cấu hình cho ImportRequestDetail
            modelBuilder.Entity<ImportRequestDetail>()
                .ToTable("tblChitiet_donnhap")
                .Property(i => i.ID).HasColumnName("ID");

            modelBuilder.Entity<ImportRequestDetail>()
                .Property(i => i.ImportRequestId).HasColumnName("iDonnhap");

            modelBuilder.Entity<ImportRequestDetail>()
                .Property(i => i.ProductId).HasColumnName("iHanghoa");

            modelBuilder.Entity<ImportRequestDetail>()
                .Property(i => i.Quantity).HasColumnName("iSoluong");

            modelBuilder.Entity<ImportRequestDetail>()
                .Property(i => i.ImportPrice).HasColumnName("fGianhap");

            // Cấu hình quan hệ cho ImportRequestDetail
            modelBuilder.Entity<ImportRequestDetail>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
} 