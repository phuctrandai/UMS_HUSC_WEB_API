using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class TinNhan
    {
        public int MaTinNhan { get; set; }
        public int MaNguoiGui { get; set; }
        public string HoTenNguoiGui { get; set; }
        public DateTime ThoiDiemGui { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public List<NguoiNhan> NguoiNhans { get; set; }

        public class NguoiNhan
        {
            public int MaNguoiNhan { get; set; }
            public string HoTenNguoiNhan { get; set; }
            public DateTime? ThoiDiemXem { get; set; }
        }
    }
}