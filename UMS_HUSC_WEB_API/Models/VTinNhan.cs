//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UMS_HUSC_WEB_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VTinNhan
    {
        public int MaTinNhan { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public System.DateTime ThoiDiemGui { get; set; }
        public int MaNguoiGui { get; set; }
        public string HoTenNguoiGui { get; set; }
        public int NguoiGuiTrangThai { get; set; }
        public int MaNguoiNhan { get; set; }
        public string HoTenNguoiNhan { get; set; }
        public Nullable<System.DateTime> ThoiDiemXem { get; set; }
        public int NguoiNhanTrangThai { get; set; }
    }
}
