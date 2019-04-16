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
        #region Tin nhan da nhan
        public static List<TinNhan> GetAllTinNhanDaNhan(string maNguoiNhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();

                var tinDaNhan = db.VTinNhanDaNhans.Where(n => n.MaNguoiNhan.Equals(maNguoiNhan) && n.DaXoa.Equals(false)).ToList();

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
            var list = GetAllTinNhanDaNhan(maNguoiNhan).Skip(skipRow).Take(soDongMoiTrang).OrderByDescending(t => t.ThoiDiemGui).ToList();

            return list;
        }

        public static long GetTongTinNhanDaNhan(string maNguoiNhan)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                return db.VTinNhanDaNhans.Where(t => t.MaNguoiNhan.Equals(maNguoiNhan)).Count();
            }
        }
            
        public static List<TinNhan> GetTinNhanNhanDaXoa(string maNguoiNhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();

                var tinDaNhan = db.VTinNhanDaNhans.Where(n => n.MaNguoiNhan.Equals(maNguoiNhan) && n.DaXoa.Equals(true)).ToList();

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

        #endregion

        #region Tin nhan da gui

        public static List<TinNhan> GetAllTinNhanDaGui(string maNguoiGui)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();

                var tinDaGui = db.VTinNhanDaGuis.Where(n => n.MaNguoiGui.Equals(maNguoiGui) && n.DaXoa.Equals(false)).ToList();

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
            var list = GetAllTinNhanDaGui(maNguoiGui).Skip(skipRow).Take(soDongMoiTrang).OrderByDescending(t => t.ThoiDiemGui).ToList();
            return list;
        }

        public static long GetTongTinNhanDaGui(string maNguoiGui)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                return db.VTinNhanDaGuis.Where(t => t.MaNguoiGui.Equals(maNguoiGui)).Count();
            }
        }

        public static List<TinNhan> GetTinNhanGuiDaXoa(string maNguoiGui)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var list = new List<TinNhan>();

                var tinDaGui = db.VTinNhanDaGuis.Where(n => n.MaNguoiGui.Equals(maNguoiGui) && n.DaXoa.Equals(true)).ToList();

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

        #endregion

        #region Tin nhan da xoa
        public static List<TinNhan> GetAllTinNhanDaXoa(string maTaiKhoan)
        {
            var list = new List<TinNhan>();

            var tinNhanNhanDaXoa = GetTinNhanNhanDaXoa(maTaiKhoan);
            var tinNhanGuiDaXoa = GetTinNhanGuiDaXoa(maTaiKhoan);

            list.AddRange(tinNhanNhanDaXoa);
            list.AddRange(tinNhanGuiDaXoa);

            return list;
        }

        public static List<TinNhan> GetTinNhanDaXoaTheoSoTrang(string maTaiKhoan, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaXoa(maTaiKhoan).Skip(skipRow).Take(soDongMoiTrang).OrderByDescending(t => t.ThoiDiemGui).ToList();

            return list;
        }

        public static long GetTongTinNhanDaXoa(string maTaiKhoan)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                return GetAllTinNhanDaXoa(maTaiKhoan).Count();
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
                var current = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(id));
                if (current != null)
                {
                    var nguoiGui = current.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui.Equals(maSinhVien));

                    if (nguoiGui != null) // sinh vien nay la nguoi gui tin nhan
                    {
                        nguoiGui.DaXoa = true;
                    }
                    else
                    {
                        var nguoiNhan = current.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan.Equals(maSinhVien));
                        if (nguoiNhan != null) nguoiNhan.DaXoa = true;
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static bool UpdateThoiDiemXem(int id, string maNguoiNhan)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var tinNhan = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(id));
                if (tinNhan != null)
                {
                    var nguoiNhan = tinNhan.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan.Equals(maNguoiNhan));
                    if (nguoiNhan != null && nguoiNhan.ThoiDiemXem == null)
                    {
                        nguoiNhan.ThoiDiemXem = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }

        public static bool ForeverDelete(int id, string maSinhVien)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var current = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(id));
                if (current != null)
                {
                    var nguoiGui = db.NGUOIGUIs.FirstOrDefault(n => n.MaNguoiGui.Equals(maSinhVien) && n.MaTinNhan.Equals(id));

                    if (nguoiGui != null) // sinh vien nay la nguoi gui tin nhan
                    {
                        db.NGUOIGUIs.Remove(nguoiGui);
                    }
                    else
                    {
                        var nguoiNhan = db.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan.Equals(maSinhVien) && n.MaTinNhan.Equals(id));
                        if (nguoiNhan != null)
                            db.NGUOINHANs.Remove(nguoiNhan);
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static int GetMaxMaTinNhan()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.TINNHANs.Max(t => t.MaTinNhan);
        }

        public static List<TinNhan.NguoiNhan> GetAllNguoiNhan(int id)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var nguoiNhans = db.NGUOINHANs.Where(n => n.MaTinNhan.Equals(id))
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