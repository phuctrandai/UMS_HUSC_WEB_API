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
    
    public partial class QUEQUAN
    {
        public string MaSinhVien { get; set; }
        public Nullable<int> QuocGia { get; set; }
        public Nullable<int> ThanhPho { get; set; }
        public Nullable<int> QuanHuyen { get; set; }
        public Nullable<int> PhuongXa { get; set; }
        public string DiaChi { get; set; }
    
        public virtual PHUONGXA PHUONGXA1 { get; set; }
        public virtual QUANHUYEN QUANHUYEN1 { get; set; }
        public virtual QUOCGIA QUOCGIA1 { get; set; }
        public virtual SINHVIEN SINHVIEN { get; set; }
        public virtual THANHPHO THANHPHO1 { get; set; }
    }
}