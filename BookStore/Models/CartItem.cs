using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartItem
    {
        public Sach Sach { get; set; }
        public int Quantity { get; set; }
    }

    public class DonHangCuaToi
    {
        public int Id { get; set; }
        public string TenSach { get; set; }
        public string HinhAnhBia { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<short> SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public short? TinhTrang { get; set; }

    }
}
