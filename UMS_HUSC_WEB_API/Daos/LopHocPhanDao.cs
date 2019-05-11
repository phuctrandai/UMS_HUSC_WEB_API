using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Daos
{
    public static class LopHocPhanDao
    {
        public static void AddLopHocPhan(string maHocPhan, int stt, int maHocKy, int maGiaoVien, DateTime ngayHetHanDK, DateTime ngayBDHoc, DateTime ngayKTHoc,
            int phongHoc, int ngayTrongTuan, int tietHocBD, int tietHocKT)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var sttStr = stt < 10 ? "00" + stt : "" + stt;
                sttStr = (stt > 10) && (stt < 99) ? "0" + stt : "" + sttStr;
                var hocKy = db.VHocKies.FirstOrDefault(i => i.MaHocKy == maHocKy);
                var hocPhan = db.HOCPHANs.FirstOrDefault(i => i.MaHocPhan.Equals(maHocPhan));

                var lopHocPhan = new LOPHOCPHAN()
                {
                    MaLopHocPhan = hocKy.NamBatDau + "-" + hocKy.NamKetThuc + "." + hocKy.TenHocKy + "." + maHocPhan + "." + sttStr,
                    MaHocPhan = maHocPhan,
                    TenLopHocPhan = hocPhan.TenHocPhan + " - Nhóm " + stt,
                    SoThuTuLop = stt,
                    HocKy = maHocKy,
                    GiangVienPhuTrach = maGiaoVien,
                    NgayHetHanDangKy = ngayHetHanDK,
                    NgayBatDauHoc = ngayBDHoc,
                    NgayKetThucHoc = ngayKTHoc,
                    HinhThucHoc = 1,
                    TrangThaiHoatDong = 1,
                    GIANGVIEN = null,
                    THOIKHOABIEUx = null,
                    LICHHOCs = null
                };

                var thoiKhoaBieu = new THOIKHOABIEU()
                {
                    MaLopHocPhan = lopHocPhan.MaLopHocPhan,
                    PhongHoc = phongHoc,
                    NgayTrongTuan = ngayTrongTuan,
                    TietHocBatDau = tietHocBD,
                    TietHocKetThuc = tietHocKT,
                    PHONGHOC1 = null,
                    LOPHOCPHAN = lopHocPhan
                };
                lopHocPhan.THOIKHOABIEUx = new List<THOIKHOABIEU> { thoiKhoaBieu };
                lopHocPhan.LICHHOCs = GetLichHoc(lopHocPhan, thoiKhoaBieu);
                db.LOPHOCPHANs.Add(lopHocPhan);
                db.SaveChanges();
            }
        }

        public static List<LICHHOC> GetLichHoc(LOPHOCPHAN lopHocPhan, THOIKHOABIEU thoiKhoaBieu)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var ngayBatDauHoc = lopHocPhan.NgayBatDauHoc;
                var ngayKetThucHoc = lopHocPhan.NgayKetThucHoc;
                var ngayTrongTuan = thoiKhoaBieu.NgayTrongTuan - 1;
                var lichHoc = new List<LICHHOC>();

                for (var index = ngayBatDauHoc; index <= ngayKetThucHoc; index = index.AddHours(24))
                {
                    if (index.DayOfWeek.GetHashCode() == ngayTrongTuan)
                    {
                        var item = new LICHHOC()
                        {
                            MaLopHocPhan = lopHocPhan.MaLopHocPhan,
                            PhongHoc = thoiKhoaBieu.PhongHoc,
                            TietHocBatDau = thoiKhoaBieu.TietHocBatDau,
                            TietHocKetThuc = thoiKhoaBieu.TietHocKetThuc,
                            NgayHoc = index
                        };
                        lichHoc.Add(item);
                    }
                }
                return lichHoc;
            }
        }

        public static List<ThoiKhoaBieu> GetLichHoc(string maLopHocPhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var maxMaHocKy = db.VHocKies.Max(i => i.MaHocKy);
                var lichHocs = db.VThoiKhoaBieux.Where(i => i.MaLopHocPhan.Equals(maLopHocPhan) && i.HocKy == maxMaHocKy)
                     .Select(i => new ThoiKhoaBieu()
                     {
                         MaSinhVien = "",
                         HoVaTen = i.HoVaTen, // Ho ten giang vien
                         MaLopHocPhan = i.MaLopHocPhan,
                         TenLopHocPhan = i.TenLopHocPhan,
                         HocKy = i.HocKy,
                         NgayHoc = i.NgayHoc,
                         PhongHoc = i.PhongHoc,
                         TenPhong = i.TenPhong,
                         TietHocBatDau = i.TietHocBatDau,
                         TietHocKetThuc = i.TietHocKetThuc
                     }).ToList();

                lichHocs.ForEach(i => i.NgayTrongTuan = i.NgayHoc.DayOfWeek.GetHashCode() + 1);

                return lichHocs;
            }
        }

        public static LOPHOCPHAN GetLopHocPhan(string maLopHocPhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.LOPHOCPHANs.FirstOrDefault(l => l.MaLopHocPhan.Equals(maLopHocPhan));
            }
        }

        public static PHONGHOC GetPhongHoc(int maPhong)
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.PHONGHOCs.FirstOrDefault(l => l.MaPhong == maPhong);
            }
        }

        public static bool TonTaiLichHoc(LICHHOC lichHoc)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var current = db.LICHHOCs.FirstOrDefault(i => i.MaLopHocPhan.Equals(lichHoc.MaLopHocPhan)
                    && i.NgayHoc.Day == lichHoc.NgayHoc.Day
                    && i.NgayHoc.Month == lichHoc.NgayHoc.Month
                    && i.NgayHoc.Year == lichHoc.NgayHoc.Year
                    && i.PhongHoc == lichHoc.PhongHoc
                    && i.TietHocBatDau == lichHoc.TietHocBatDau 
                    && i.TietHocKetThuc == lichHoc.TietHocKetThuc);
                return current != null;
            }
        }

        public static GIANGVIEN GetGiangVien(int maGiangVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.GIANGVIENs.FirstOrDefault(l => l.MaGiangVien == maGiangVien);
            }
        }

        public static void AddLichHoc(LICHHOC lichHoc)
        {
            using (var db = new UMS_HUSCEntities())
            {
                db.LICHHOCs.Add(lichHoc);
                db.SaveChanges();
            }
        }

        public static void DangKyLop(string maSinhVien, string maLopHocPhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                DANGKYTHEOHOC dangKy = new DANGKYTHEOHOC()
                {
                    MaLopHocPhan = maLopHocPhan,
                    MaSinhVien = maSinhVien,
                    HinhThucDangKy = 1,
                    LanHoc = 1,
                    ThoiDiemDangKy = DateTime.Now,
                    ThoiDiemXuLy = DateTime.Now,
                    TinhTichLuyTinChi = true,
                    TrangThaiXuLy = true,
                    LOPHOCPHAN = null,
                    SINHVIEN = null
                };
                db.DANGKYTHEOHOCs.Add(dangKy);
                db.SaveChanges();
            }
        }

        public static int MaxMaHocKy()
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.VHocKies.Max(i => i.MaHocKy);
            }
        }
    }
}