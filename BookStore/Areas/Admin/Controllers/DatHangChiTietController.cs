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

    public class DatHangChiTietController : Controller
    {
        private readonly ShopSachContext _context;

        public DatHangChiTietController(ShopSachContext context)
        {
            _context = context;
        }

        // GET: DatHangChiTiet

        public ActionResult Index(int IdDonHang)
        {
            var datHangChiTiets = _context.DatHangChiTiets.Where(pig => pig.DatHangId == IdDonHang).Include(d => d.DatHang).Include(d => d.Sach);
            ViewBag.pigg = _context.DatHangs.Include(t => t.KhachHang).FirstOrDefault(t => t.Id == IdDonHang);

            return View(datHangChiTiets.ToList());
        }
        /*public async Task<IActionResult> Index()
        {
            var shopSachContext = _context.DatHangChiTiets.Include(d => d.DatHang).Include(d => d.Sach);
            return View(await shopSachContext.ToListAsync());
        }*/

        // GET: DatHangChiTiet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHangChiTiet = await _context.DatHangChiTiets
                .Include(d => d.DatHang)
                .Include(d => d.Sach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHangChiTiet == null)
            {
                return NotFound();
            }

            return View(datHangChiTiet);
        }

        // GET: DatHangChiTiet/Create
        public IActionResult Create()
        {
            ViewData["DatHangId"] = new SelectList(_context.DatHangs, "Id", "Id");
/*            ViewData["SachId"] = new SelectList(_context.Saches, "Id", "TenSach");
*/            return View();
        }

        // POST: DatHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatHangId,SachId,SoLuong,DonGia")] DatHangChiTiet datHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datHangChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DatHangId"] = new SelectList(_context.DatHangs, "Id", "Id", datHangChiTiet.DatHangId);
/*            ViewData["SachId"] = new SelectList(_context.Saches, "Id", "TenSach", datHangChiTiet.SachId);
*/            return View(datHangChiTiet);
        }

        // GET: DatHangChiTiet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHangChiTiet = await _context.DatHangChiTiets.FindAsync(id);
            if (datHangChiTiet == null)
            {
                return NotFound();
            }
            ViewData["DatHangId"] = new SelectList(_context.DatHangs, "Id", "Id", datHangChiTiet.DatHangId);
/*            ViewData["SachId"] = new SelectList(_context.Saches, "Id", "TenSach", datHangChiTiet.SachId);
*/            return View(datHangChiTiet);
        }

        // POST: DatHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatHangId,SachId,SoLuong,DonGia")] DatHangChiTiet datHangChiTiet)
        {
            if (id != datHangChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datHangChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatHangChiTietExists(datHangChiTiet.Id))
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
            ViewData["DatHangId"] = new SelectList(_context.DatHangs, "Id", "Id", datHangChiTiet.DatHangId);
/*            ViewData["SachId"] = new SelectList(_context.Saches, "Id", "TenSach", datHangChiTiet.SachId);
*/            return View(datHangChiTiet);
        }

        // GET: DatHangChiTiet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var datHangChiTiet = await _context.DatHangChiTiets
                .Include(d => d.DatHang)
                .Include(d => d.Sach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datHangChiTiet == null)
            {
                return NotFound();
            }

            return View(datHangChiTiet);
        }

        // POST: DatHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datHangChiTiet = await _context.DatHangChiTiets.FindAsync(id);
            _context.DatHangChiTiets.Remove(datHangChiTiet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatHangChiTietExists(int id)
        {
            return _context.DatHangChiTiets.Any(e => e.Id == id);
        }
    }
}
