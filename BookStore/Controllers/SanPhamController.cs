using BookStore.Data;
using BookStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class SanPhamController : Controller
    {
        private ShopSachContext db = new ShopSachContext();
        private readonly ShopSachContext _context;
        private readonly IProduct _product;
        public SanPhamController(ShopSachContext context, IProduct product)

        {
            _context = context;
            _product = product;


        }
        public IActionResult Index(string timkiem, int? page = 0)
        {

            int limit = 12;
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
            .Skip((int)((page - 1) * limit)).Take(limit);

            if (!String.IsNullOrEmpty(timkiem))
            {
                dress = dress.Where(s => s.TenSach.Contains(timkiem));
            }

            return View(dress);
        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.NhaXuatBan)
                .Include(s => s.LoaiSach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }
    }
}
