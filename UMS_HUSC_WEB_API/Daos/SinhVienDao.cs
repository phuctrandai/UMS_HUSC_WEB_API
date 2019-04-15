using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static void DoiMatKhau(string maSinhVien, string matKhau)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.SINHVIENs.FirstOrDefault(
                sv => sv.MaSinhVien.ToLower().Equals(maSinhVien.ToLower())
                && sv.MatKhau.Equals(matKhau));
            if (current != null)
            {
                current.MatKhau = matKhau;
                db.SaveChanges();
            }
        }

        public static VLyLichCaNhan GetLyLichCaNhan(string maSinhVien)
        {
            var current = new VLyLichCaNhan
            {
                ThongTinChung = GetThongTinChung(maSinhVien),
                ThongTinLienHe = ThongTinLienHe(maSinhVien),
                ThuongTru = ThuongTru(maSinhVien),
                QueQuan = QueQuan(maSinhVien),
                DacDiemBanThan = DacDiemBanThan(maSinhVien),
                LichSuBanThan = LichSuBanThan(maSinhVien)
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
            thongTin.QuocTich = current.QuocTich;
            thongTin.DanToc = current.DanToc;
            thongTin.TonGiao = current.TonGiao;
            return thongTin;
        }

        public static bool UpdateThongTinChung(ref ThongTinChung thongTinChung)
        {
            using (var db = new UMS_HUSCEntities())
            {
                string maSinhVien = thongTinChung.MaSinhVien;
                var current = db.THONGTINCHUNGs.FirstOrDefault(t => t.MaSinhVien.Equals(maSinhVien));
                if (current != null)
                {
                    if (IsValidDateString(thongTinChung.NgaySinh.ToString()))
                    {
                        current.NgaySinh = DateTime.Parse(thongTinChung.NgaySinh.ToString());
                        thongTinChung.NgaySinh = current.NgaySinh;
                    }

                    if (IsValidDateString(thongTinChung.NgayCap.ToString()))
                    {
                        current.NgayCap = DateTime.Parse(thongTinChung.NgayCap.ToString());
                        thongTinChung.NgayCap = current.NgayCap;
                    }

                    if (!string.IsNullOrEmpty(thongTinChung.SoCMND))
                        current.SoCMND = thongTinChung.SoCMND;

                    if (!string.IsNullOrEmpty(thongTinChung.NoiCap))
                        current.NoiCap = thongTinChung.NoiCap;

                    if (thongTinChung.QuocTich > 0)
                        current.QuocTich = thongTinChung.QuocTich;

                    if (thongTinChung.TonGiao > 0)
                        current.TonGiao = thongTinChung.TonGiao;

                    if (thongTinChung.DanToc > 0)
                        current.DanToc = thongTinChung.DanToc;

                    if (!string.IsNullOrEmpty(thongTinChung.AnhDaiDien))
                        current.AnhDaiDien = thongTinChung.AnhDaiDien;
                }

                var noiSinh = db.NOISINHs.FirstOrDefault(n => n.MaSinhVien.Equals(maSinhVien));
                if (noiSinh != null && thongTinChung.NoiSinh != null)
                {
                    if (thongTinChung.NoiSinh.ThanhPho > 0)
                        noiSinh.ThanhPho = thongTinChung.NoiSinh.ThanhPho;

                    if (thongTinChung.NoiSinh.QuocGia > 0)
                        noiSinh.QuocGia = thongTinChung.NoiSinh.QuocGia;
                }
                else if (noiSinh == null)
                {
                    NOISINH newNoiSinh = new NOISINH()
                    {
                        MaSinhVien = maSinhVien,
                        QUOCGIA1 = null,
                        SINHVIEN = null,
                        THANHPHO1 = null
                    };
                    if (thongTinChung.NoiSinh.ThanhPho > 0)
                        newNoiSinh.ThanhPho = thongTinChung.NoiSinh.ThanhPho;

                    if (thongTinChung.NoiSinh.QuocGia > 0)
                        newNoiSinh.QuocGia = thongTinChung.NoiSinh.QuocGia;

                    db.NOISINHs.Add(newNoiSinh);
                }

                if (current != null)
                {
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
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

        public static VLichSuBanThan LichSuBanThan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VLichSuBanThans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }

        public static List<VThongTinChung> GetHoTenVaMaSinhVien()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.VThongTinChungs.ToList();
        }

        private static bool IsValidDateString(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime result))
                return true;
            return false;
        }
    }
}