using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace BookStore.Controllers
{
    [Area("Admin")]

    public class SachController : Controller
    {
        public INotyfService _notifyService { get; }

        private readonly ShopSachContext _context;

        public SachController(ShopSachContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;

        }

        // GET: Sach
        public async Task<IActionResult> Index()
        {
            var shopSachContext = _context.Saches.Include(s => s.LoaiSach).Include(s => s.NhaXuatBan);
            return View(await shopSachContext.ToListAsync());
        }

        // GET: Sach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.LoaiSach)
                .Include(s => s.NhaXuatBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Sach/Create
        public IActionResult Create()
        {
            ViewData["NhaXuatBanId"] = new SelectList(_context.NhaXuatBans, "Id", "TenNhaXuatBan");
            ViewData["LoaiSachId"] = new SelectList(_context.LoaiSaches, "Id", "TenLoai");
            return View();
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,NhaXuatBanId,LoaiSachId,TenSach,DonGia,SoLuong,HinhAnhBia,MoTa")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                sach.HinhAnhBia = Upload(file);
                _context.Add(sach);
                await _context.SaveChangesAsync();
                _notifyService.Success("Thêm sản phẩm thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiSachId"] = new SelectList(_context.LoaiSaches, "Id", "TenLoai", sach.LoaiSachId);
            ViewData["NhaXuatBanId"] = new SelectList(_context.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBanId);
            return View(sach);
        }

        // GET: Sach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            ViewData["LoaiSachId"] = new SelectList(_context.LoaiSaches, "Id", "TenLoai", sach.LoaiSachId);
            ViewData["NhaXuatBanId"] = new SelectList(_context.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBanId);
            return View(sach);
        }

        // POST: Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,NhaXuatBanId,LoaiSachId,TenSach,DonGia,SoLuong,HinhAnhBia,MoTa")] Sach sach)
        {
            if (id != sach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sach.HinhAnhBia = Upload(file);
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiSachId"] = new SelectList(_context.LoaiSaches, "Id", "TenLoai", sach.LoaiSachId);
            ViewData["NhaXuatBanId"] = new SelectList(_context.NhaXuatBans, "Id", "TenNhaXuatBan", sach.NhaXuatBanId);
            return View(sach);
        }

        // GET: Sach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Saches
                .Include(s => s.LoaiSach)
                .Include(s => s.NhaXuatBan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _context.Saches.FindAsync(id);
            _context.Saches.Remove(sach);
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa sản phẩm thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
            return _context.Saches.Any(e => e.Id == id);
        }


        public string Upload(IFormFile file)
        {
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\{ UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                
            }
            return UploadFileName;
        }
    }
}
