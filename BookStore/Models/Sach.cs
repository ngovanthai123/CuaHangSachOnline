using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("Sach")]
    public partial class Sach
    {
        public Sach()
        {
            DatHangChiTiets = new HashSet<DatHangChiTiet>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NhaXuatBan_ID")]
        public int? NhaXuatBanId { get; set; }
        [Column("LoaiSach_ID")]
        public int? LoaiSachId { get; set; }
        [StringLength(255)]
        [Display(Name ="Tên sách")]
        public string TenSach { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int DonGia { get; set; }

        [Display(Name = "Số lượng")]
        public int? SoLuong { get; set; }
        [StringLength(255)]
        public string HinhAnhBia { get; set; }
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [ForeignKey(nameof(LoaiSachId))]
        [InverseProperty("Saches")]
        public virtual LoaiSach LoaiSach { get; set; }
        [ForeignKey(nameof(NhaXuatBanId))]
        [InverseProperty("Saches")]
        public virtual NhaXuatBan NhaXuatBan { get; set; }
        [InverseProperty(nameof(DatHangChiTiet.Sach))]
        public virtual ICollection<DatHangChiTiet> DatHangChiTiets { get; set; }
    }
}
