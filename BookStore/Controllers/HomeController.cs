using AspNetCoreHero.ToastNotification.Abstractions;
using BookStore.Data;
using BookStore.Library;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {

        public INotyfService _notifyService { get; }

        const string SessionMand = "_Id";
        const string SessionHoten = "_HoVaTen";
        const string SessionTenDN = "_TenDN";

        public const string CARTKEY = "shopcart";
        private readonly ILogger<HomeController> _logger;
        private readonly ShopSachContext _context;
        private ShopSachContext db = new ShopSachContext();

        public HomeController(ILogger<HomeController> logger, ShopSachContext context, INotyfService notifyService)
        {
            _logger = logger;
            _context = context;
            _notifyService = notifyService;

        }

        public  IActionResult Index(int? page = 0)
        {
            
            return View();
        }

       /* public async Task<IActionResult> SachIndex()
        {
            return View(await _context.Saches.ToListAsync());
        }*/

        // GET: Home/Login
        public ActionResult Login()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHangLogin khachHang)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(khachHang.MatKhau);
                var taiKhoan = db.KhachHangs.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == mahoamatkhau).SingleOrDefault();

                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View(khachHang);
                }
                else
                {
                    // Đăng ký SESSION

                    HttpContext.Session.SetInt32(SessionMand, taiKhoan.Id);
                    HttpContext.Session.SetString(SessionHoten, taiKhoan.HoVaTen);
                    HttpContext.Session.SetString(SessionTenDN, taiKhoan.TenDangNhap);


                    // Quay về trang chủ
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(khachHang);
        }


        // GET: Home/Login_Cart
        public ActionResult Login_Cart()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }
        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login_Cart(KhachHangLogin khachHang)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(khachHang.MatKhau);
                var taiKhoan = db.KhachHangs.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == mahoamatkhau).SingleOrDefault();

                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View(khachHang);
                }
                else
                {
                    // Đăng ký SESSION

                    HttpContext.Session.SetInt32(SessionMand, taiKhoan.Id);
                    HttpContext.Session.SetString(SessionHoten, taiKhoan.HoVaTen);
                    HttpContext.Session.SetString(SessionTenDN, taiKhoan.TenDangNhap);


                    // Quay về trang chủ
                    return RedirectToAction("Index", "SanPham");
                }
            }

            return View(khachHang);
        }

        public ActionResult Logout()
        {
            // Xóa SESSION
            HttpContext.Session.Remove("_Id");
            HttpContext.Session.Remove("_Hoten");
            HttpContext.Session.Remove("_TenDN");
            HttpContext.Session.Remove(CARTKEY);

            // Quy về trang chủ
            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,HoVaTen,DienThoai,DiaChi,Email,TenDangNhap,MatKhau,XacNhanMatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            { 
             var check = db.KhachHangs.FirstOrDefault(r => r.TenDangNhap == khachHang.TenDangNhap);
            if (check==null)
            {

                khachHang.MatKhau = SHA1.ComputeHash(khachHang.MatKhau);
                khachHang.XacNhanMatKhau = SHA1.ComputeHash(khachHang.XacNhanMatKhau);
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                    ModelState.AddModelError("SignUpError", "Đăng ký thành công!");
                    return RedirectToAction(nameof(Login));

            }
            else
            {
                ViewBag.error = "Tên đăng nhập đã tồn tại !!!";
                return View();
            }
            }
            return View();
        }
        public IActionResult SignUp()
        {
            ModelState.AddModelError("SignUpError", "");
            return View();
        }


        // change password
        public ActionResult ChangePassword()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                int id = (int)HttpContext.Session.GetInt32("_Id");

                string matKhauMaHoa = SHA1.ComputeHash(model.MatKhauCu);
                string XacNhanMatKhauMaHoa = SHA1.ComputeHash(model.XacNhanMatKhau);
                string matkhaumoi;
                var taiKhoan = _context.KhachHangs.Where(r => r.Id == id && r.MatKhau == matKhauMaHoa).SingleOrDefault();
                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Mật khẩu cũ không chính xác!");
                    return View(model);
                }
                else
                {
                    matkhaumoi = SHA1.ComputeHash(model.MatKhauMoi);
                    KhachHang n = _context.KhachHangs.Find(id);
                    n.MatKhau = matkhaumoi;
                    n.XacNhanMatKhau = matkhaumoi;

                    _context.Entry(n).State = EntityState.Modified;
                    _context.SaveChanges();

                    ModelState.AddModelError("LoginError", "Đổi mật khẩu thành công!");
                    
                }
            }
            return RedirectToAction("Logout", "Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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


        // đọc danh sách mat hang trong giỏ từ session
        public List<CartItem> GetCartItems()
        {
            //var session = HttpContext.Session;
            //string jsoncart = session.GetString("shopcart");
            var jsoncart = HttpContext.Request.Cookies[HttpContext.Session.GetInt32("_Id").ToString()];
            if (!string.IsNullOrEmpty(jsoncart))
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);

            }
            return new List<CartItem>();
        }
        // lưu danh sách mặt hàng trong giỏ vào session 
        void SaveCartSession(List<CartItem> lst)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(lst);
            //session.SetString("shopcart", jsoncart);
            HttpContext.Response.Cookies.Append(HttpContext.Session.GetInt32(SessionMand).ToString(), jsoncart);
        }



        // cho vao gio
        public async Task<IActionResult> AddToCart(int id)
        {
            if (HttpContext.Session.GetInt32("_Id") == null)
            {
                return RedirectToAction("Login_Cart", "Home");
            }
            else
            {
                var product = await _context.Saches
                .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound("Sản phẩm không tồn tại");
                }
                var cart = GetCartItems();
                var item = cart.Find(p => p.Sach.Id == id);
                if (item != null)
                {
                    item.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem() { Sach = product, Quantity = 1 });
                }
                SaveCartSession(cart);
                return RedirectToAction(nameof(Cart));
            }


        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sach.Id == id);
            if (item != null)
            {
                item.Quantity = quantity;
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        public IActionResult RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sach.Id == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // action goi view gio hang
        public IActionResult Cart()
        {

            return View(GetCartItems());
        }

        public IActionResult CheckOut()
        {
            
            return View(GetCartItems());
        }

        //lưu thông tin hóa đơn
        [HttpPost]
        public async Task<IActionResult> CreateBill(int cusname, string cusphone, string cusadd)
        {

            var session = HttpContext.Session;
            var b = new DatHang();
            b.NgayDatHang = DateTime.Now;
            b.KhachHangId = HttpContext.Session.GetInt32(SessionMand);
            b.DienThoaiGiaoHang = cusphone;
            b.DiaChiGiaoHang = cusadd;
            b.TinhTrang = 0;
            _context.Add(b);
            await _context.SaveChangesAsync();
            //them billdetails
            var cart = GetCartItems();
            int amount = 0;
            int total = 0;

            foreach (var i in cart)
            {
                var d = new DatHangChiTiet();
                d.DatHangId = b.Id;
                d.SachId = i.Sach.Id;
                var sanpham = _context.Saches.FirstOrDefault(t => t.Id == d.SachId);
                sanpham.SoLuong -= i.Quantity;
                await _context.SaveChangesAsync();
                d.DonGia = i.Sach.DonGia;
                d.SoLuong = Convert.ToInt16(i.Quantity);

                amount = i.Sach.DonGia * i.Quantity;
                total += amount;
                _context.Add(d);
            }

            await _context.SaveChangesAsync();

            HttpContext.Response.Cookies.Append(HttpContext.Session.GetInt32(SessionMand).ToString(), "");

            //chuyển hướng 
            _notifyService.Success("Cảm ơn bạn đã mua sản phẩm tại BookStore!!!");
            return RedirectToAction("Index", "SanPham");

        }
        public IActionResult Thanks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseConfirmation(DatHang DatHang)
        {
            if (ModelState.IsValid)
            {
                var session = HttpContext.Session;
                // Lưu vào bảng dathang
                DatHang dh = new DatHang();
                dh.DiaChiGiaoHang = DatHang.DiaChiGiaoHang;
                dh.DienThoaiGiaoHang = DatHang.DienThoaiGiaoHang;
                dh.NgayDatHang = DateTime.Now;

                dh.KhachHangId = Convert.ToInt32(session.GetInt32("_Id"));
                dh.TinhTrang = 0;
                db.DatHangs.Add(dh);
                db.SaveChanges();

                ///* Lưu vào bảng */DatHang_ChiTiet
                var cart = GetCartItems();
                foreach (var item in cart)
                {
                    DatHangChiTiet chitiet = new DatHangChiTiet();
                    chitiet.DatHangId = dh.Id;
                    chitiet.SachId = item.Sach.Id;
                    chitiet.SoLuong = Convert.ToInt16(item.Quantity);
                    chitiet.DonGia = item.Sach.DonGia;
                    db.DatHangChiTiets.Add(chitiet);
                    db.SaveChanges();
                }

                // Xóa giỏ hàng
                cart.Clear();

                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }

            return View(DatHang);
        }

        public ActionResult DonHangCuaToi()
        {
            int makh = Convert.ToInt32(HttpContext.Session.GetInt32("_Id").ToString());
            var DonHangCuaToi = (from sp in db.Saches
                                 join chitiet in db.DatHangChiTiets on sp.Id equals chitiet.SachId
                                 join dhang in db.DatHangs on chitiet.DatHangId equals dhang.Id
                                 join kh in db.KhachHangs on dhang.KhachHangId equals kh.Id
                                 where (kh.Id == makh)

                                 select new DonHangCuaToi()
                                 {
                                     TenSach = sp.TenSach,
                                     HinhAnhBia = sp.HinhAnhBia,
                                     DonGia = chitiet.DonGia,
                                     Id = kh.Id,
                                     SoLuong = chitiet.SoLuong,
                                     NgayDatHang = dhang.NgayDatHang,
                                     TinhTrang = dhang.TinhTrang
                                 }).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

            return View(DonHangCuaToi);
        }

    }
}
