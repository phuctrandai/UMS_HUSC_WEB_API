using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Daos
{
    public static class TinNhanDao
    {
        public static readonly int TINNHAN_CHUA_XOA = 0;
        public static readonly int TINNHAN_TAM_XOA = 1;
        public static readonly int TINNHAN_XOA_HAN = 2;

        #region Tin nhan da nhan
        public static List<TinNhan> GetAllTinNhanDaNhan(string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                var tinDaNhan = db.VTinNhanDaNhans.Where(n => n.MaNguoiNhan == maTaiKhoan && n.TrangThai == TINNHAN_CHUA_XOA).ToList();

                foreach (var item in tinDaNhan)
                {
                    string temp = HttpUtility.HtmlDecode(item.NoiDung);
                    item.NoiDung = temp.Replace("\r\n", "")
                                        .Replace("style=\"", "style='")
                                        .Replace("href=\"", "href='")
                                        .Replace("\">", "'>")
                                        .Replace(";\"", ";'")
                                        .Replace("\"", "&quot;");
                    item.TieuDe = HttpUtility.HtmlDecode(item.TieuDe).Replace("\r\n", "");

                    TinNhan tinNhan = new TinNhan()
                    {
                        MaTinNhan = item.MaTinNhan,
                        MaNguoiGui = item.MaNguoiGui,
                        HoTenNguoiGui = item.HoTenNguoiGui,
                        TieuDe = item.TieuDe,
                        NoiDung = item.NoiDung,
                        ThoiDiemGui = item.ThoiDiemGui,
                        NguoiNhans = GetAllNguoiNhan(item.MaTinNhan)
                    };

                    list.Add(tinNhan);
                }
                return list.OrderBy(t => t.ThoiDiemGui).ToList();
            }
        }

        public static List<TinNhan> GetTinNhanDaNhanTheoSoTrang(string maNguoiNhan, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaNhan(maNguoiNhan).OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDongMoiTrang).ToList();

            return list;
        }

        public static long GetTongTinNhanDaNhan(string maSinhVien)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                return db.VTinNhanDaNhans.Where(t => t.MaNguoiNhan.Equals(maTaiKhoan)).Count();
            }
        }
            
        public static List<TinNhan> GetTinNhanNhanDaXoa(string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                var tinDaNhan = db.VTinNhanDaNhans.Where(n => n.MaNguoiNhan == maTaiKhoan && n.TrangThai == TINNHAN_TAM_XOA).ToList();

                foreach (var item in tinDaNhan)
                {
                    string temp = HttpUtility.HtmlDecode(item.NoiDung);
                    item.NoiDung = temp.Replace("\r\n", "")
                                        .Replace("style=\"", "style='")
                                        .Replace("href=\"", "href='")
                                        .Replace("\">", "'>")
                                        .Replace(";\"", ";'")
                                        .Replace("\"", "&quot;");
                    item.TieuDe = HttpUtility.HtmlDecode(item.TieuDe).Replace("\r\n", "");

                    TinNhan tinNhan = new TinNhan()
                    {
                        MaTinNhan = item.MaTinNhan,
                        MaNguoiGui = item.MaNguoiGui,
                        HoTenNguoiGui = item.HoTenNguoiGui,
                        TieuDe = item.TieuDe,
                        NoiDung = item.NoiDung,
                        ThoiDiemGui = item.ThoiDiemGui,
                        NguoiNhans = GetAllNguoiNhan(item.MaTinNhan)
                    };

                    list.Add(tinNhan);
                }
                return list.OrderBy(t => t.ThoiDiemGui).ToList();
            }
        }

        public static List<TinNhan> SearchTinNhanDaNhan(string maNguoiNhan, string keyword, int soTrang, int soDong)
        {
            using(var db = new UMS_HUSCEntities())
            {
                var skipRow = (soTrang - 1) * soDong;
                var keywordLower = keyword.ToLower();
                var all = GetAllTinNhanDaNhan(maNguoiNhan);
                var result = all.Where(t => t.TieuDe.ToLower().Contains(keywordLower) ||
                                t.HoTenNguoiGui.ToLower().Contains(keywordLower));

                return result.OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDong).ToList();
            }
        }
        #endregion

        #region Tin nhan da gui

        public static List<TinNhan> GetAllTinNhanDaGui(string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                var tinDaGui = db.VTinNhanDaGuis.Where(n => n.MaNguoiGui == maTaiKhoan && n.TrangThai == TINNHAN_CHUA_XOA).ToList();

                foreach (var item in tinDaGui)
                {
                    string temp = HttpUtility.HtmlDecode(item.NoiDung);
                    item.NoiDung = temp.Replace("\r\n", "")
                                        .Replace("style=\"", "style='")
                                        .Replace("href=\"", "href='")
                                        .Replace("\">", "'>")
                                        .Replace(";\"", ";'")
                                        .Replace("\"", "&quot;");
                    item.TieuDe = HttpUtility.HtmlDecode(item.TieuDe).Replace("\r\n", "");

                    TinNhan tinNhan = new TinNhan()
                    {
                        MaTinNhan = item.MaTinNhan,
                        MaNguoiGui = item.MaNguoiGui,
                        HoTenNguoiGui = item.HoTenNguoiGui,
                        TieuDe = item.TieuDe,
                        NoiDung = item.NoiDung,
                        ThoiDiemGui = item.ThoiDiemGui,
                        NguoiNhans = GetAllNguoiNhan(item.MaTinNhan)
                    };

                    list.Add(tinNhan);
                }
                return list.OrderBy(t => t.ThoiDiemGui).ToList();
            }
        }

        public static List<TinNhan> GetTinNhanDaGuiTheoSoTrang(string maNguoiGui, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaGui(maNguoiGui).OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDongMoiTrang).ToList();
            return list;
        }

        public static long GetTongTinNhanDaGui(string maSinhVien)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                return db.VTinNhanDaGuis.Where(t => t.MaNguoiGui == maTaiKhoan).Count();
            }
        }

        public static List<TinNhan> GetTinNhanGuiDaXoa(string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();
                var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                var tinDaGui = db.VTinNhanDaGuis.Where(n => n.MaNguoiGui == maTaiKhoan && n.TrangThai == TINNHAN_TAM_XOA).ToList();

                foreach (var item in tinDaGui)
                {
                    string temp = HttpUtility.HtmlDecode(item.NoiDung);
                    item.NoiDung = temp.Replace("\r\n", "")
                                        .Replace("style=\"", "style='")
                                        .Replace("href=\"", "href='")
                                        .Replace("\">", "'>")
                                        .Replace(";\"", ";'")
                                        .Replace("\"", "&quot;");
                    item.TieuDe = HttpUtility.HtmlDecode(item.TieuDe).Replace("\r\n", "");

                    TinNhan tinNhan = new TinNhan()
                    {
                        MaTinNhan = item.MaTinNhan,
                        MaNguoiGui = item.MaNguoiGui,
                        HoTenNguoiGui = item.HoTenNguoiGui,
                        TieuDe = item.TieuDe,
                        NoiDung = item.NoiDung,
                        ThoiDiemGui = item.ThoiDiemGui,
                        NguoiNhans = GetAllNguoiNhan(item.MaTinNhan)
                    };

                    list.Add(tinNhan);
                }
                return list.OrderBy(t => t.ThoiDiemGui).ToList();
            }
        }

        public static List<TinNhan> SearchTinNhanDaGui(string maNguoiGui, string keyword, int soTrang, int soDong)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var skipRow = (soTrang - 1) * soDong;
                var keywordLower = keyword.ToLower();
                var all = GetAllTinNhanDaGui(maNguoiGui);
                var result = all.Where(t => t.TieuDe.ToLower().Contains(keywordLower)
                                || t.HoTenNguoiGui.ToLower().Contains(keywordLower));

                return result.OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDong).ToList();
            }
        }
        #endregion

        #region Tin nhan da xoa
        public static List<TinNhan> GetAllTinNhanDaXoa(string maSinhVien)
        {
            var list = new List<TinNhan>();
            
            var tinNhanNhanDaXoa = GetTinNhanNhanDaXoa(maSinhVien);
            var tinNhanGuiDaXoa = GetTinNhanGuiDaXoa(maSinhVien);

            list.AddRange(tinNhanNhanDaXoa);
            list.AddRange(tinNhanGuiDaXoa);

            return list;
        }

        public static List<TinNhan> GetTinNhanDaXoaTheoSoTrang(string maSinhVien, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaXoa(maSinhVien).OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDongMoiTrang).ToList();

            return list;
        }

        public static long GetTongTinNhanDaXoa(string maSinhVien)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                return GetAllTinNhanDaXoa(maSinhVien).Count();
            }
        }

        public static List<TinNhan> SearchTinNhanDaXoa(string maSinhVien, string keyword, int soTrang, int soDong)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var skipRow = (soTrang - 1) * soDong;
                var keywordLower = keyword.ToLower();
                var all = GetAllTinNhanDaXoa(maSinhVien);
                var result = all.Where(t => t.TieuDe.ToLower().Contains(keywordLower) ||
                                t.HoTenNguoiGui.ToLower().Contains(keywordLower));

                return result.OrderByDescending(t => t.ThoiDiemGui).Skip(skipRow).Take(soDong).ToList();
            }
        }
        #endregion

        public static TinNhan GetTinNhanTheoId(int id)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var tn = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(id));

                if (tn == null) return null;

                string temp = HttpUtility.HtmlDecode(tn.NoiDung);
                tn.NoiDung = temp.Replace("\r\n", "")
                                    .Replace("style=\"", "style='")
                                    .Replace("href=\"", "href='")
                                    .Replace("\">", "'>")
                                    .Replace(";\"", ";'")
                                    .Replace("\"", "&quot;");
                tn.TieuDe = HttpUtility.HtmlDecode(tn.TieuDe).Replace("\r\n", "");
                return new TinNhan()
                {
                    MaTinNhan = tn.MaTinNhan,
                    MaNguoiGui = tn.NGUOIGUIs.ElementAt(0).MaNguoiGui,
                    HoTenNguoiGui = tn.NGUOIGUIs.ElementAt(0).HoTenNguoiGui,
                    TieuDe = tn.TieuDe,
                    NoiDung = tn.NoiDung,
                    ThoiDiemGui = tn.ThoiDiemGui,
                    NguoiNhans = GetAllNguoiNhan(tn.MaTinNhan)
                };
            }
        }

        public static void AddTinNhan(TINNHAN tinNhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                db.TINNHANs.Add(tinNhan);
                db.SaveChanges();
            }
        }

        public static bool AttempDeleteTinNhan(int id, string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var current = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan == id);
                if (current != null)
                {
                    var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                    var nguoiGui = current.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui == maTaiKhoan);

                    if (nguoiGui != null) // sinh vien nay la nguoi gui tin nhan
                    {
                        nguoiGui.TrangThai = TINNHAN_TAM_XOA;
                    }
                    else
                    {
                        var nguoiNhan = current.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan == maTaiKhoan);
                        if (nguoiNhan != null) nguoiNhan.TrangThai = TINNHAN_TAM_XOA;
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool UpdateThoiDiemXem(int id, string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var tinNhan = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan == id);
                if (tinNhan != null)
                {
                    var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                    var nguoiNhan = db.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan == maTaiKhoan && n.MaTinNhan == id);
                    //var nguoiGui = db.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui == maTaiKhoan && n.MaTinNhan == id);
                    if (nguoiNhan != null && nguoiNhan.ThoiDiemXem == null)
                    {
                        nguoiNhan.ThoiDiemXem = DateTime.Now;
                        db.SaveChanges();
                    }
                    return (nguoiNhan != null) /*|| (nguoiGui != null)*/;
                }
                return false;
            }
        }

        public static bool ForeverDelete(int id, string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var current = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan == id);
                if (current != null)
                {
                    var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                    var nguoiGui = db.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui == maTaiKhoan && n.MaTinNhan == id);

                    if (nguoiGui != null) // sinh vien nay la nguoi gui tin nhan
                    {
                        nguoiGui.TrangThai = TINNHAN_XOA_HAN;
                    }
                    else
                    {
                        var nguoiNhan = db.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan == maTaiKhoan && n.MaTinNhan == id);
                        if (nguoiNhan != null)
                            nguoiNhan.TrangThai = TINNHAN_XOA_HAN;
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool RestoreTinNhan(int id, string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var current = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan == id);
                if (current != null)
                {
                    var maTaiKhoan = SinhVienDao.GetMaTaiKhoan(maSinhVien);
                    var nguoiGui = db.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui == maTaiKhoan && n.MaTinNhan == id);

                    if (nguoiGui != null) // sinh vien nay la nguoi gui tin nhan
                    {
                        nguoiGui.TrangThai = TINNHAN_CHUA_XOA;
                    }
                    else
                    {
                        var nguoiNhan = db.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan == maTaiKhoan && n.MaTinNhan == id);
                        if (nguoiNhan != null)
                            nguoiNhan.TrangThai = TINNHAN_CHUA_XOA;
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static int GetMaxMaTinNhan()
        {
            using (var db = new UMS_HUSCEntities())
            {
                return db.TINNHANs.Max(t => t.MaTinNhan);
            }
        }

        public static List<TinNhan.NguoiNhan> GetAllNguoiNhan(int id)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var nguoiNhans = db.NGUOINHANs.Where(n => n.MaTinNhan == id)
                    .Select(n => new TinNhan.NguoiNhan()
                    {
                        MaNguoiNhan = n.MaNguoiNhan,
                        HoTenNguoiNhan = n.HoTenNguoiNhan,
                        ThoiDiemXem = n.ThoiDiemXem
                    }).ToList();

                return nguoiNhans;
            }
        }

    }
}