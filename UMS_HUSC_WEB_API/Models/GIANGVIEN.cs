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
    
    public partial class GIANGVIEN
    {
        public int MaGiangVien { get; set; }
        public string HoVaTen { get; set; }
        public Nullable<int> MaTaiKhoan { get; set; }
    
        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}