using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using BookStore.Library;
using Microsoft.AspNetCore.Http;
using System.IO;
using BookStore.Data;

namespace BookStore.Controllers
{
    [Area("Admin")]

    public class NhanVienController : Controller
    {
        public INotyfService _notifyService { get; }
        private readonly ShopSachContext _context;

        public NhanVienController(ShopSachContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhanViens.ToListAsync());
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,HoVaTen,DienThoai,DiaChi,Email,HinhAnh,TenDangNhap,MatKhau,Quyen,XacNhanMatKhau")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.HinhAnh = Upload(file);
                nhanVien.MatKhau = SHA1.ComputeHash(nhanVien.MatKhau);
                nhanVien.XacNhanMatKhau = SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                _notifyService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,HoVaTen,DienThoai,DiaChi,Email,HinhAnh,TenDangNhap,MatKhau,Quyen,XacNhanMatKhau")] NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nhanVien.HinhAnh = Upload(file);
                    nhanVien.MatKhau = SHA1.ComputeHash(nhanVien.MatKhau);
                    nhanVien.XacNhanMatKhau = SHA1.ComputeHash(nhanVien.XacNhanMatKhau);
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Cập nhật thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Id))
                    {
                        _notifyService.Success("Có lỗi xảy ra");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
            _notifyService.Success("Xóa tài khoản thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.Id == id);
        }


        public string Upload(IFormFile file)
        {
            var path = "";
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = $"images\\{Guid.NewGuid()}_{file.FileName}";
                path = $"wwwroot\\{UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return UploadFileName;
        }
    }
}
