using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Keyless]
    [Table("ThuKho")]
    public partial class ThuKho
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(100)]
        public string HoVaTen { get; set; }
        [StringLength(20)]
        public string DienThoai { get; set; }
        [StringLength(255)]
        public string DiaChi { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string TenDangNhap { get; set; }
        [StringLength(255)]
        public string MatKhau { get; set; }
    }
}
