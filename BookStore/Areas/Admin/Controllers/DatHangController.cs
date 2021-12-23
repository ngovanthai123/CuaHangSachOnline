using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Area("Admin")]
    public class DatHangController : Controller
    {
        private readonly ShopSachContext _context;

        public DatHangController(ShopSachContext context)
        {
            _context = context;
        }

        // GET: DatHang
        public async Task<IActionResult> Index()
        {
            var shopSachContext = _context.DatHangs.Include(d => d.KhachHang).Include(d => d.NhanVien);
            return View(await shopSachContext.ToListAsync());
        }

        // GET: DatHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // GET: DatHang/Create
        public IActionResult Create()
        {
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "HoVaTen");
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "Id", "HoVaTen");
            return View();
        }

        // POST: DatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NhanVienId,KhachHangId,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "HoVaTen", datHang.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "Id", "HoVaTen", datHang.NhanVienId);
            return View(datHang);
        }

        // GET: DatHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs.FindAsync(id);
            if (datHang == null)
            {
                return NotFound();
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "HoVaTen", datHang.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "Id", "HoVaTen", datHang.NhanVienId);
            return View(datHang);
        }

        // POST: DatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NhanVienId,KhachHangId,DienThoaiGiaoHang,DiaChiGiaoHang,NgayDatHang,TinhTrang")] DatHang datHang)
        {
            if (id != datHang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHangExists(datHang.Id))
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
            ViewData["KhachHangId"] = new SelectList(_context.KhachHangs, "Id", "HoVaTen", datHang.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanViens, "Id", "HoVaTen", datHang.NhanVienId);
            return View(datHang);
        }

        // GET: DatHang/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHang = await _context.DatHangs
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHang == null)
            {
                return NotFound();
            }

            return View(datHang);
        }

        // POST: DatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datHang = await _context.DatHangs.FindAsync(id);
            _context.DatHangs.Remove(datHang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatHangExists(int id)
        {
            return _context.DatHangs.Any(e => e.Id == id);
        }
    }
}
