﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UMS_HUSCEntities : DbContext
    {
        public UMS_HUSCEntities()
            : base("name=UMS_HUSCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<VThongTinChung> VThongTinChungs { get; set; }
        public virtual DbSet<VThongTinLienHe> VThongTinLienHes { get; set; }
        public virtual DbSet<VDacDiemBanThan> VDacDiemBanThans { get; set; }
        public virtual DbSet<VQueQuan> VQueQuans { get; set; }
        public virtual DbSet<VThuongTru> VThuongTrus { get; set; }
        public virtual DbSet<LICHSUBANTHAN> LICHSUBANTHANs { get; set; }
        public virtual DbSet<VThongTinCaNhan> VThongTinCaNhans { get; set; }
        public virtual DbSet<VNoiSinh> VNoiSinhs { get; set; }
        public virtual DbSet<THONGBAO> THONGBAOs { get; set; }
        public virtual DbSet<FIREBASE> FIREBASEs { get; set; }
        public virtual DbSet<NGUOINHAN> NGUOINHANs { get; set; }
        public virtual DbSet<TINNHAN> TINNHANs { get; set; }
    }
}
