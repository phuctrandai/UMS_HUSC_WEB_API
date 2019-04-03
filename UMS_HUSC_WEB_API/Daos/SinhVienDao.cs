using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Daos
{
    public static class SinhVienDao
    {

        public static bool TonTaiSinhVien(string maSinhVien, string matKhau)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return GetSinhVien(maSinhVien, matKhau) == null ? false : true;
        }

        public static SINHVIEN GetSinhVien(string maSinhVien, string matKhau)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.SINHVIENs.FirstOrDefault(
                sv => sv.MaSinhVien.ToLower().Equals(maSinhVien.ToLower())
                && sv.MatKhau.Equals(matKhau));
        }

        public static VLyLichCaNhan GetLyLichCaNhan(string maSinhVien)
        {
            var current = new VLyLichCaNhan
            {
                ThongTinChung = SinhVienDao.GetThongTinChung(maSinhVien),
                ThongTinLienHe = SinhVienDao.ThongTinLienHe(maSinhVien),
                ThuongTru = SinhVienDao.ThuongTru(maSinhVien),
                QueQuan = SinhVienDao.QueQuan(maSinhVien),
                DacDiemBanThan = SinhVienDao.DacDiemBanThan(maSinhVien),
                LichSuBanThan = SinhVienDao.LichSuBanThan(maSinhVien)
            };
            return current;
        }

        public static VThongTinCaNhan GetThongTinCaNhan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.VThongTinCaNhans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
        }

        public static ThongTinChung GetThongTinChung(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            ThongTinChung thongTin = new ThongTinChung();
            var current = db.VThongTinChungs.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            thongTin.MaSinhVien = current.MaSinhVien;
            thongTin.HoTen = current.HoTen;
            thongTin.GioiTinh = current.GioiTinh;
            thongTin.NgaySinh = current.NgaySinh;
            thongTin.TenDanToc = current.TenDanToc;
            thongTin.TenQuocGia = current.TenQuocGia;
            thongTin.TenTonGiao = current.TenTonGiao;
            thongTin.SoCMND = current.SoCMND;
            thongTin.NoiCap = current.NoiCap;
            thongTin.NgayCap = current.NgayCap;
            thongTin.NoiSinh = db.VNoiSinhs.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return thongTin;
        }
        
        public static VThongTinLienHe ThongTinLienHe(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VThongTinLienHes.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }
        
        public static VThuongTru ThuongTru(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VThuongTrus.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }
        
        public static VQueQuan QueQuan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VQueQuans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }
        
        public static VDacDiemBanThan DacDiemBanThan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VDacDiemBanThans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }
        
        public static LICHSUBANTHAN LichSuBanThan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.LICHSUBANTHANs.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            current.SINHVIEN = null;
            return current;
        }

        public static List<VThongTinChung> GetHoTenVaMaSinhVien()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.VThongTinChungs.ToList();
        }
    }
}