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
    
    public partial class THOIKHOABIEU
    {
        public string MaLopHocPhan { get; set; }
        public int PhongHoc { get; set; }
        public int TietHocBatDau { get; set; }
        public int TietHocKetThuc { get; set; }
        public int NgayTrongTuan { get; set; }
    
        public virtual LOPHOCPHAN LOPHOCPHAN { get; set; }
        public virtual PHONGHOC PHONGHOC1 { get; set; }
    }
}
