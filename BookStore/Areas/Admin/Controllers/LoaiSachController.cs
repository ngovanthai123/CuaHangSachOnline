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

    public class LoaiSachController : Controller
    {
        private readonly ShopSachContext _context;

        public LoaiSachController(ShopSachContext context)
        {
            _context = context;
        }

        // GET: LoaiSach
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiSaches.ToListAsync());
        }

        // GET: LoaiSach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiSach == null)
            {
                return NotFound();
            }

            return View(loaiSach);
        }

        // GET: LoaiSach/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiSach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenLoai")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiSach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiSach);
        }

        // GET: LoaiSach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches.FindAsync(id);
            if (loaiSach == null)
            {
                return NotFound();
            }
            return View(loaiSach);
        }

        // POST: LoaiSach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLoai")] LoaiSach loaiSach)
        {
            if (id != loaiSach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiSachExists(loaiSach.Id))
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
            return View(loaiSach);
        }

        // GET: LoaiSach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiSach = await _context.LoaiSaches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiSach == null)
            {
                return NotFound();
            }

            return View(loaiSach);
        }

        // POST: LoaiSach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiSach = await _context.LoaiSaches.FindAsync(id);
            _context.LoaiSaches.Remove(loaiSach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiSachExists(int id)
        {
            return _context.LoaiSaches.Any(e => e.Id == id);
        }
    }
}
