using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("DatHang_ChiTiet")]
    public partial class DatHangChiTiet
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DatHang_ID")]
        public int? DatHangId { get; set; }
        [Column("Sach_ID")]
        public int? SachId { get; set; }
        public short SoLuong { get; set; }
        public int DonGia { get; set; }

        [ForeignKey(nameof(DatHangId))]
        [InverseProperty("DatHangChiTiets")]
        public virtual DatHang DatHang { get; set; }
        [ForeignKey(nameof(SachId))]
        [InverseProperty("DatHangChiTiets")]
        public virtual Sach Sach { get; set; }
    }
}
