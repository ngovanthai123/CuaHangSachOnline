using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("NhanVien")]
    public partial class NhanVien
    {
        public NhanVien()
        {
            DatHangs = new HashSet<DatHang>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string HoVaTen { get; set; }
        [StringLength(20)]
        public string DienThoai { get; set; }
        [StringLength(255)]
        public string DiaChi { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(50)]
        public string TenDangNhap { get; set; }
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        public int Quyen { get; set; }
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }

        public string HinhAnh { get; set; }

        [InverseProperty(nameof(DatHang.NhanVien))]
        public virtual ICollection<DatHang> DatHangs { get; set; }
    }

    public class ManegeLogin
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }

    public class ChangePasswordNV
    {
        [Display(Name = "Mật khẩu cũ")]
        [DataType(DataType.Password)]
        public string MatKhauCu { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }
}
