using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class KhachHangEdit
    {
        public KhachHangEdit()
        {

        }

        public KhachHangEdit(KhachHangEdit kh)
        {
            ID = kh.ID;
            HoVaTen = kh.HoVaTen;
            SoDienThoai = kh.SoDienThoai;
            DiaChi = kh.DiaChi;
            TenDangNhap = kh.TenDangNhap;
            MatKhau = kh.MatKhau;
            XacNhanMatKhau = kh.XacNhanMatKhau;
            Email = kh.Email;
        }
        public int ID { get; set; }

        [Display(Name = "Họ và tên")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Trường Họ và tên phải khớp với biểu thức chính quy '^ [A-Z] + [a-zA-Z/s] * $'.")]
        public string HoVaTen { get; set; }

        [Display(GroupName = "Điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
        public string Email { get; set; }
    }

    public class KhachHangSignUp
    {
        public int ID { get; set; }
        [Display(Name = "Họ và tên")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Trường Họ và tên phải khớp với biểu thức chính quy '^ [A-Z] + [a-zA-Z/s] * $'.")]
        public string HoVaTen { get; set; }


        [Display(GroupName = "Điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        public string DienThoai { get; set; }


        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
        
        public string Email { get; set; }
    }

    public class KhachHangLogin
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }



        internal static Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
    public class ChangePassword
    {
        [Display(Name = "Mật khẩu cũ")]
        [DataType(DataType.Password)]
        public string MatKhauCu { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }
}
