using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookStore.Models;

#nullable disable

namespace BookStore.Data
{
    public partial class ShopSachContext : DbContext
    {
        public ShopSachContext()
        {
        }

        public ShopSachContext(DbContextOptions<ShopSachContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatHang> DatHangs { get; set; }
        public virtual DbSet<DatHangChiTiet> DatHangChiTiets { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiSach> LoaiSaches { get; set; }
        public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Sach> Saches { get; set; }
        public virtual DbSet<ThuKho> ThuKhos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/                optionsBuilder.UseSqlServer("Server=LAPTOP-R5QB9QIO;Database=ShopSach;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<DatHang>(entity =>
            {
                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DatHangs)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("FK_DatHang_KhachHang");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.DatHangs)
                    .HasForeignKey(d => d.NhanVienId)
                    .HasConstraintName("FK_DatHang_NhanVien");
            });

            modelBuilder.Entity<DatHangChiTiet>(entity =>
            {
                entity.HasOne(d => d.DatHang)
                    .WithMany(p => p.DatHangChiTiets)
                    .HasForeignKey(d => d.DatHangId)
                    .HasConstraintName("FK_DatHang_ChiTiet_DatHang");

                entity.HasOne(d => d.Sach)
                    .WithMany(p => p.DatHangChiTiets)
                    .HasForeignKey(d => d.SachId)
                    .HasConstraintName("FK_DatHang_ChiTiet_Sach");
            });

            modelBuilder.Entity<Sach>(entity =>
            {
                entity.HasOne(d => d.LoaiSach)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.LoaiSachId)
                    .HasConstraintName("FK_Sach_LoaiSach");

                entity.HasOne(d => d.NhaXuatBan)
                    .WithMany(p => p.Saches)
                    .HasForeignKey(d => d.NhaXuatBanId)
                    .HasConstraintName("FK_Sach_NhaXuatBan");
            });

            modelBuilder.Entity<ThuKho>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
