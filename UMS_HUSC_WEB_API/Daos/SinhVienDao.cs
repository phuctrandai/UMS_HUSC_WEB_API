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
            using (var db = new UMS_HUSCEntities())
            {
                return GetSinhVien(maSinhVien, matKhau) == null ? false : true;
            }
        }

        public static SINHVIEN GetSinhVien(string maSinhVien, string matKhau)
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.SINHVIENs.Include("TAIKHOAN").FirstOrDefault(
                    sv => sv.MaSinhVien.ToLower().Equals(maSinhVien.ToLower()) && sv.TAIKHOAN.MatKhau.Equals(matKhau));
            }
        }

        public static bool DoiMatKhau(string maSinhVien, string matKhau, string matKhauMoi)
        {
            if (string.IsNullOrEmpty(matKhauMoi)) return false;

            using (var db = new UMS_HUSCEntities())
            {
                var current = db.SINHVIENs.Include("TAIKHOAN").FirstOrDefault(
                    sv => sv.MaSinhVien.ToLower().Equals(maSinhVien.ToLower()) && sv.TAIKHOAN.MatKhau.Equals(matKhau));

                if (current != null)
                {
                    current.TAIKHOAN.MatKhau = matKhauMoi;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static VLyLichCaNhan GetLyLichCaNhan(string maSinhVien)
        {
            var current = new VLyLichCaNhan
            {
                ThongTinChung = GetThongTinChung(maSinhVien),
                ThongTinLienHe = ThongTinLienHe(maSinhVien),
                ThuongTru = ThuongTru(maSinhVien),
                QueQuan = GetQueQuan(maSinhVien),
                DacDiemBanThan = GetDacDiemBanThan(maSinhVien),
                LichSuBanThan = GetLichSuBanThan(maSinhVien)
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

        public static bool UpdateThongTinChung(ThongTinChung thongTinChung)
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

                    current.SoCMND = thongTinChung.SoCMND ?? string.Empty;

                    current.NoiCap = thongTinChung.NoiCap ?? string.Empty;

                    current.QuocTich = thongTinChung.QuocTich > 0 ? thongTinChung.QuocTich : null;

                    current.TonGiao = thongTinChung.TonGiao > 0 ? thongTinChung.TonGiao : null;

                    current.DanToc = thongTinChung.DanToc > 0 ? thongTinChung.DanToc : null;

                    if (!string.IsNullOrEmpty(thongTinChung.AnhDaiDien))
                        current.AnhDaiDien = thongTinChung.AnhDaiDien;
                }

                var noiSinh = db.NOISINHs.FirstOrDefault(n => n.MaSinhVien.Equals(maSinhVien));
                if (noiSinh != null && thongTinChung.NoiSinh != null)
                {
                    noiSinh.ThanhPho = thongTinChung.NoiSinh.ThanhPho > 0 ? thongTinChung.NoiSinh.ThanhPho : null;

                    noiSinh.QuocGia = thongTinChung.NoiSinh.QuocGia > 0 ? thongTinChung.NoiSinh.QuocGia : null;
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
                    newNoiSinh.ThanhPho = thongTinChung.NoiSinh.ThanhPho > 0 ? thongTinChung.NoiSinh.ThanhPho : null;

                    newNoiSinh.QuocGia = thongTinChung.NoiSinh.QuocGia > 0 ? thongTinChung.NoiSinh.QuocGia : null;

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
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                var current = db.VThongTinLienHes.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
                return current;
            }
        }

        public static void UpDateThongTinLienHe(string maSinhVien, VThongTinLienHe thongTinLienHe)
        {
            if (thongTinLienHe == null) return;

            using (var db = new UMS_HUSCEntities())
            {
                THONGTINLIENHE newObj;
                var current = db.THONGTINLIENHEs.FirstOrDefault(t => t.MaSinhVien.Equals(maSinhVien));

                if (current != null)
                {
                    newObj = current;
                    newObj.DiDong = thongTinLienHe.DiDong ?? string.Empty;

                    newObj.DienThoai = thongTinLienHe.DienThoai ?? string.Empty;

                    newObj.Email = thongTinLienHe.Email ?? string.Empty;

                    if (IsValidDateString(thongTinLienHe.NgayBatDauCuTru.ToString()))
                        newObj.NgayBatDauCuTru = DateTime.Parse(thongTinLienHe.NgayBatDauCuTru.ToString());

                    newObj.HinhThucCuTru = thongTinLienHe.HinhThucCuTru;
                }
                else
                {
                    newObj = new THONGTINLIENHE()
                    {
                        MaSinhVien = maSinhVien,
                        DiDong = thongTinLienHe.DiDong ?? string.Empty,
                        DienThoai = thongTinLienHe.DienThoai ?? string.Empty,
                        Email = thongTinLienHe.Email ?? string.Empty,
                        NgayBatDauCuTru = IsValidDateString(thongTinLienHe.NgayBatDauCuTru.ToString()) ? DateTime.Parse(thongTinLienHe.NgayBatDauCuTru.ToString()) : (DateTime?)null,

                        HinhThucCuTru = thongTinLienHe.HinhThucCuTru,
                        PHUONGXA1 = null,
                        QUANHUYEN1 = null,
                        QUOCGIA1 = null,
                        SINHVIEN = null,
                        THANHPHO1 = null
                    };
                }

                if (thongTinLienHe.HinhThucCuTru != null)
                {
                    switch (thongTinLienHe.HinhThucCuTru)
                    {
                        case 0: // Chua khoi tao
                            newObj.DiaChi = null;
                            newObj.QuocGia = null;
                            newObj.ThanhPho = null;
                            newObj.QuanHuyen = null;
                            newObj.PhuongXa = null;
                            newObj.KyTucXa = null;
                            break;
                        case 1: // Noi tru
                            newObj.DiaChi = null;
                            newObj.QuocGia = null;
                            newObj.ThanhPho = null;
                            newObj.PhuongXa = null;
                            newObj.QuanHuyen = null;
                            newObj.KyTucXa = thongTinLienHe.MaKyTucXa;
                            break;
                        case 2: // Ngoai tru
                            newObj.DiaChi = thongTinLienHe.DiaChi ?? string.Empty;
                            newObj.QuocGia = thongTinLienHe.MaQuocGia;
                            newObj.ThanhPho = thongTinLienHe.MaThanhPho;
                            newObj.QuanHuyen = thongTinLienHe.MaQuanHuyen;
                            newObj.PhuongXa = thongTinLienHe.MaPhuongXa;
                            newObj.KyTucXa = null;
                            break;
                        case 3: // Theo ho khau thuong tru
                            newObj.QuocGia = null;
                            newObj.ThanhPho = null;
                            newObj.QuanHuyen = null;
                            newObj.PhuongXa = null;
                            newObj.KyTucXa = null;
                            break;
                    }
                }

                if (current == null)
                    db.THONGTINLIENHEs.Add(newObj);

                db.SaveChanges();
            }
        }

        public static VThuongTru ThuongTru(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VThuongTrus.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }

        public static void UpdateThongTinThuongTru(string maSinhVien, VThuongTru thuongTru)
        {
            if (thuongTru == null) return;

            using (var db = new UMS_HUSCEntities())
            {
                var current = db.THUONGTRUs.FirstOrDefault(t => t.MaSinhVien.Equals(maSinhVien));
                if (current != null)
                {
                    current.QuocGia = thuongTru.MaQuocGia > 0 ? thuongTru.MaQuocGia : null;
                    current.ThanhPho = thuongTru.MaThanhPho > 0 ? thuongTru.MaThanhPho : null;
                    current.QuanHuyen = thuongTru.MaQuanHuyen > 0 ? thuongTru.MaQuanHuyen : null;
                    current.PhuongXa = thuongTru.MaPhuongXa > 0 ? thuongTru.MaPhuongXa : null;
                    current.DiaChi = thuongTru.DiaChi ?? string.Empty;
                }
                else
                {
                    THUONGTRU newObj = new THUONGTRU()
                    {
                        MaSinhVien = maSinhVien,
                        QuocGia = thuongTru.MaQuocGia > 0 ? thuongTru.MaQuocGia : null,
                        ThanhPho = thuongTru.MaThanhPho > 0 ? thuongTru.MaThanhPho : null,
                        QuanHuyen = thuongTru.MaQuanHuyen > 0 ? thuongTru.MaQuanHuyen : null,
                        PhuongXa = thuongTru.MaPhuongXa > 0 ? thuongTru.MaPhuongXa : null,
                        DiaChi = thuongTru.DiaChi ?? string.Empty,
                        SINHVIEN = null,
                        PHUONGXA1 = null,
                        QUANHUYEN1 = null,
                        QUOCGIA1 = null,
                        THANHPHO1 = null
                    };
                    db.THUONGTRUs.Add(newObj);
                }
                db.SaveChanges();
            }
        }

        public static VQueQuan GetQueQuan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VQueQuans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }

        public static void UpdateQueQuan(string maSinhVien, VQueQuan queQuan)
        {
            if (queQuan == null) return;

            using (var db = new UMS_HUSCEntities())
            {
                var current = db.QUEQUANs.FirstOrDefault(q => q.MaSinhVien.Equals(maSinhVien));
                if (current != null)
                {
                    current.QuocGia = queQuan.MaQuocGia > 0 ? queQuan.MaQuocGia : null;
                    current.ThanhPho = queQuan.MaThanhPho > 0 ? queQuan.MaThanhPho : null;
                    current.QuanHuyen = queQuan.MaQuanHuyen > 0 ? queQuan.MaQuanHuyen : null;
                    current.PhuongXa = queQuan.MaPhuongXa > 0 ? queQuan.MaPhuongXa : null;
                    current.DiaChi = queQuan.DiaChi ?? string.Empty;
                }
                else
                {
                    QUEQUAN newObj = new QUEQUAN()
                    {
                        MaSinhVien = maSinhVien,
                        QuocGia = queQuan.MaQuocGia > 0 ? queQuan.MaQuocGia : null,
                        ThanhPho = queQuan.MaThanhPho > 0 ? queQuan.MaThanhPho : null,
                        QuanHuyen = queQuan.MaQuanHuyen > 0 ? queQuan.MaQuanHuyen : null,
                        PhuongXa = queQuan.MaPhuongXa > 0 ? queQuan.MaPhuongXa : null,
                        DiaChi = queQuan.DiaChi ?? string.Empty,
                        SINHVIEN = null,
                        PHUONGXA1 = null,
                        QUANHUYEN1 = null,
                        QUOCGIA1 = null,
                        THANHPHO1 = null
                    };
                }
                db.SaveChanges();
            }
        }

        public static VDacDiemBanThan GetDacDiemBanThan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VDacDiemBanThans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }

        public static VLichSuBanThan GetLichSuBanThan(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.VLichSuBanThans.FirstOrDefault(v => v.MaSinhVien.Equals(maSinhVien));
            return current;
        }

        public static int GetMaTaiKhoan(string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var maTaiKhoan = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien.Equals(maSinhVien)).MaTaiKhoan;
                return maTaiKhoan.Value;
            }
        }

        private static bool IsValidDateString(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out DateTime result))
                return true;
            return false;
        }

        public static string GetHoTenTheoTaiKhoan(int maTaiKhoan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var giangVien = db.GIANGVIENs.FirstOrDefault(g => g.MaTaiKhoan == maTaiKhoan);
                if (giangVien != null)
                {
                    return giangVien.HoVaTen;
                }
                else
                {
                    var sinhVien = db.SINHVIENs.Include("THONGTINCHUNG").FirstOrDefault(s => s.MaTaiKhoan == maTaiKhoan);
                    return sinhVien.THONGTINCHUNG.HoTen;
                }
            }
        }

        public static List<Select2Model> GetMaTaiKhoanVaHoTen()
        {
            using (var db = new UMS_HUSCEntities())
            {
                var sinhviens = db.VThongTinCaNhans.Select(s => new Select2Model()
                {
                    id = s.MaTaiKhoan,
                    text = "SV: " + s.HoTen + " --- MSV: " + s.MaSinhVien
                }).ToList();
                var giangviens = db.GIANGVIENs.Select(g => new Select2Model() { id = g.MaTaiKhoan.Value, text = "GV: " + g.HoVaTen }).ToList();
                sinhviens.AddRange(giangviens);
                return sinhviens;
            }
        }

        public static List<TaiKhoan> GetMaTaiKhoanVaHoTen(string nameQuery, int soTrang, int soDong)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var sinhViens = db.VThongTinCaNhans.Where(s => s.HoTen.ToLower().Trim().Contains(nameQuery.ToLower().Trim()))
                    .Select(s => new TaiKhoan()
                    {
                        MaTaiKhoan = s.MaTaiKhoan,
                        HoTen = s.HoTen,
                        MaSinhVien = s.MaSinhVien
                    }).ToList();

                var giangViens = db.GIANGVIENs.Where(g => g.HoVaTen.ToLower().Trim().Contains(nameQuery.ToLower().Trim()))
                    .Select(g => new TaiKhoan()
                    {
                        MaTaiKhoan = g.MaTaiKhoan.Value,
                        HoTen = g.HoVaTen,
                        MaSinhVien = ""
                    }).ToList();
                sinhViens.AddRange(giangViens);

                var skipRow = (soTrang - 1) * soDong;

                return sinhViens.Skip(skipRow).Take(soDong).ToList();
            }
        }
    }
}