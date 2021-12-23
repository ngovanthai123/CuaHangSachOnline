using BookStore.Data;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class NgoaiNgu : Controller
    {
        private ShopSachContext db = new ShopSachContext();
        private readonly ShopSachContext _context;
        private readonly IProduct _product;
        public NgoaiNgu(ShopSachContext context, IProduct product)

        {
            _context = context;
            _product = product;


        }
        public IActionResult Index(string timkiem, int? page = 0)
        {

            int limit = 8;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;


            int totalProduct = _product.totalProduct();

            ViewBag.totalProduct = totalProduct;

            ViewBag.numberPage = _product.numberPage(totalProduct, limit);
            var dress = _context.Saches
                .Where(sp => sp.LoaiSachId == 8)
            .Skip((int)((page - 1) * limit)).Take(limit);

            if (!String.IsNullOrEmpty(timkiem))
            {
                dress = dress.Where(s => s.TenSach.Contains(timkiem));
            }
            return View(dress);
        }
    }
}
