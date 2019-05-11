using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class ThoiKhoaBieu
    {
        public string MaLopHocPhan { get; set; }
        public string TenLopHocPhan { get; set; }
        public string TenPhong { get; set; }
        public int TietHocBatDau { get; set; }
        public int TietHocKetThuc { get; set; }
        public int PhongHoc { get; set; }
        public string HoVaTen { get; set; }
        public DateTime NgayHoc { get; set; }
        public string MaSinhVien { get; set; }
        public int HocKy { get; set; }
        public int NgayTrongTuan { get; set; }
    }
}