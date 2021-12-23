using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("LoaiSach")]
    public partial class LoaiSach
    {
        public LoaiSach()
        {
            Saches = new HashSet<Sach>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string TenLoai { get; set; }

        [InverseProperty(nameof(Sach.LoaiSach))]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
