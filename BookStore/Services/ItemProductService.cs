using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
namespace BookStore.Services
{
    public class  ItemProductService  : IProduct
    {
        private readonly ShopSachContext _db;
        private List<Sach> products = new List<Sach>();
        public ItemProductService (ShopSachContext db)
        {
            _db = db;
            this.products = _db.Saches.ToList();
        }
        public IEnumerable<Sach> getProductAll()
        {
            return products;
        }
        public int totalProduct()
        {
            return products.Count + 1;
        }
        public int numberPage(int totalProduct, int limit)
        {
            float numberpage = ((float)totalProduct) / ((float)limit);
            return (int)Math.Ceiling(numberpage);
        }
        public IEnumerable<Sach> paginationProduct(int start, int limit)
        {
            var sanpham = (from s in _db.Saches select s);
            var dataProduct = sanpham.OrderByDescending(x => x.Id).Skip(start).Take(limit);
            return dataProduct.ToList();
        }
    }

}
