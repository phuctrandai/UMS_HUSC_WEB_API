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
    
    public partial class THONGTINCHUNG
    {
        public string HoTen { get; set; }
        public Nullable<bool> GioiTinh { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<int> QuocTich { get; set; }
        public Nullable<int> DanToc { get; set; }
        public string SoCMND { get; set; }
        public string NoiCap { get; set; }
        public Nullable<System.DateTime> NgayCap { get; set; }
        public Nullable<int> TonGiao { get; set; }
        public string AnhDaiDien { get; set; }
        public string MaSinhVien { get; set; }
    
        public virtual DANTOC DANTOC1 { get; set; }
        public virtual QUOCGIA QUOCGIA { get; set; }
        public virtual SINHVIEN SINHVIEN { get; set; }
        public virtual TONGIAO TONGIAO1 { get; set; }
    }
}
