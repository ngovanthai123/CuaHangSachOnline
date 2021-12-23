using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("KhachHang")]
    public partial class KhachHang
    {
        public KhachHang()
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
        [StringLength(50)]
        public string TenDangNhap { get; set; }
        [StringLength(255)]
        public string MatKhau { get; set; }
        [StringLength(255)]

        [Display(Name ="Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
        [StringLength(255)]

        public string Email { get; set; }

        [InverseProperty(nameof(DatHang.KhachHang))]
        public virtual ICollection<DatHang> DatHangs { get; set; }
    }
}
