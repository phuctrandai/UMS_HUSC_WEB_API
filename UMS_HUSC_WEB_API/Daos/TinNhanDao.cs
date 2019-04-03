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
        public static List<TINNHAN> GetAllTinNhanDaNhan(string maNguoiNhan)
        {
            var db = new UMS_HUSCEntities();
            var list = new List<TINNHAN>();

            var tinDaNhan = db.NGUOINHANs.Where(n => n.MaNguoiNhan.Equals(maNguoiNhan)).ToList();
            foreach (var item in tinDaNhan)
            {
                var tinNhan = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(item.MaTinNhan));
                string temp = HttpUtility.HtmlDecode(tinNhan.NoiDung);
                tinNhan.NoiDung = temp.Replace("\r\n", "")
                                    .Replace("style=\"", "style='")
                                    .Replace("href=\"", "href='")
                                    .Replace("\">", "'>")
                                    .Replace(";\"", ";'")
                                    .Replace("\"", "&quot;");
                temp = HttpUtility.HtmlDecode(tinNhan.TieuDe).Replace("\r\n", "");
                tinNhan.TieuDe = temp;
                list.Add(tinNhan);
            }
            return list.OrderBy(t => t.ThoiDiemGui).ToList();
        }

        public static List<TINNHAN> GetAllTinNhanDaGui(string maNguoiGui)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var list = new List<TINNHAN>();

            List<TINNHAN> tinDaGui = db.TINNHANs.Where(t => t.MaNguoiGui.Equals(maNguoiGui)).ToList();
            foreach (var item in tinDaGui)
            {
                string temp = HttpUtility.HtmlDecode(item.NoiDung);
                item.NoiDung = temp.Replace("\r\n", "")
                                    .Replace("style=\"", "style='")
                                    .Replace("href=\"", "href='")
                                    .Replace("\">", "'>")
                                    .Replace(";\"", ";'")
                                    .Replace("\"", "&quot;");
                temp = HttpUtility.HtmlDecode(item.TieuDe).Replace("\r\n", "");
                item.TieuDe = temp;
                list.Add(item);
            }
            return list.OrderBy(t => t.ThoiDiemGui).ToList();
        }

        public static List<TINNHAN> GetAllTinNhanDaXoa(string maTaiKhoan)
        {

            return null;
        }

        public static List<TINNHAN> GetTinNhanDaNhanTheoSoTrang(string maNguoiNhan, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaNhan(maNguoiNhan).Skip(skipRow).Take(soDongMoiTrang).ToList();
            return list;
        }

        public static List<TINNHAN> GetTinNhanDaGuiTheoSoTrang(string maNguoiGui, int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllTinNhanDaGui(maNguoiGui).Skip(skipRow).Take(soDongMoiTrang).ToList();
            return list;
        }

        public static TINNHAN GetTinNhanTheoId(int id, string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var tn = db.TINNHANs.FirstOrDefault(t => t.MaTinNhan.Equals(id));
            if (tn != null)
            {
                var check = tn.NGUOINHANs.FirstOrDefault(n => n.MaNguoiNhan.Equals(maSinhVien));
                if (check != null)
                {
                    string temp = HttpUtility.HtmlDecode(tn.NoiDung);
                    tn.NoiDung = temp.Replace("\r\n", "")
                                        .Replace("style=\"", "style='")
                                        .Replace("href=\"", "href='")
                                        .Replace("\">", "'>")
                                        .Replace(";\"", ";'")
                                        .Replace("\"", "&quot;");
                    temp = HttpUtility.HtmlDecode(tn.TieuDe).Replace("\r\n", "");
                    tn.TieuDe = temp;
                    return tn;
                }
            }
            return null;
        }

        public static void AddTinNhan(TINNHAN tinNhan)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            db.TINNHANs.Add(tinNhan);
            db.SaveChanges();
        }

        public static void AddNguoiNhan(List<NGUOINHAN> danhSachNguoiNhan)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            db.NGUOINHANs.AddRange(danhSachNguoiNhan);
            db.SaveChanges();
        }

        public static List<NGUOINHAN> GetAllNguoiNhan()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.NGUOINHANs.ToList();
        }

        public static int GetMaxMaTinNhan()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.TINNHANs.Max(t => t.MaTinNhan);
        }

        public static long GetTongTinNhanDaNhan(string maNguoiNhan)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.NGUOINHANs.Where(t => t.MaNguoiNhan.Equals(maNguoiNhan)).Count();
        }

        public static long GetTongTinNhanDaGui(string maNguoiGui)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.TINNHANs.Where(t => t.MaNguoiGui.Equals(maNguoiGui)).Count();
        }
    }
}