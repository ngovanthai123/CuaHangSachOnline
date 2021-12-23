using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BookStore.Models
{
    [Table("NhaXuatBan")]
    public partial class NhaXuatBan
    {
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string TenNhaXuatBan { get; set; }

        [InverseProperty(nameof(Sach.NhaXuatBan))]
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
