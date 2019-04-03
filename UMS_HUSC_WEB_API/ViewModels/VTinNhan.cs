using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class VTinNhan
    {
        public TINNHAN TinNhan { get; set; }
        public List<String> DanhSachTenNguoiNhan { get; set; }
    }
}