using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("DatHang")]
    public partial class DatHang
    {
        public DatHang()
        {
            DatHangChiTiets = new HashSet<DatHangChiTiet>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NhanVien_ID")]
        public int? NhanVienId { get; set; }
        [Column("KhachHang_ID")]
        public int? KhachHangId { get; set; }
        [StringLength(20)]
        public string DienThoaiGiaoHang { get; set; }
        [StringLength(255)]
        public string DiaChiGiaoHang { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NgayDatHang { get; set; }
        public short? TinhTrang { get; set; }

        [ForeignKey(nameof(KhachHangId))]
        [InverseProperty("DatHangs")]
        public virtual KhachHang KhachHang { get; set; }
        [ForeignKey(nameof(NhanVienId))]
        [InverseProperty("DatHangs")]
        public virtual NhanVien NhanVien { get; set; }
        [InverseProperty(nameof(DatHangChiTiet.DatHang))]
        public virtual ICollection<DatHangChiTiet> DatHangChiTiets { get; set; }
    }
}
