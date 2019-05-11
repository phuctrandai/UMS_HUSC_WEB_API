using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class ThongTinCaNhan
    {
        public string MaSinhVien { get; set; }
        public string TenNganh { get; set; }
        public string KhoaHoc { get; set; }
        public string HoTen { get; set; }
        public string AnhDaiDien { get; set; }
        public int MaTaiKhoan { get; set; }
        public VHocKy HocKyTacNghiep { get; set; }
    }
}