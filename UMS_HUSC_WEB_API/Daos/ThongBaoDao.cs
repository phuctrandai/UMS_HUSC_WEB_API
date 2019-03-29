using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.Daos
{
    public class ThongBaoDao
    {
        public static List<THONGBAO> GetAllThongBao()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            List<THONGBAO> listThongBao = db.THONGBAOs.OrderByDescending(v => v.ThoiGianDang).ToList();
            listThongBao.ForEach(x => {
                string temp = HttpUtility.HtmlDecode(x.NoiDung);
                x.NoiDung = temp.Replace("\r\n", "")
                                .Replace("style=\"", "style='")
                                .Replace("href=\"", "href='")
                                .Replace("\">", "'>")
                                .Replace(";\"", ";'")
                                .Replace("\"", "&quot;");

                temp = HttpUtility.HtmlDecode(x.TieuDe).Replace("\r\n", "");
                x.TieuDe = temp;
            });

            return listThongBao;
        }

        public static THONGBAO GetThongBaoTheoId(int id)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            var current = db.THONGBAOs.FirstOrDefault(t => t.MaThongBao.Equals(id));
            if (current != null)
            {
                string temp = HttpUtility.HtmlDecode(current.NoiDung);
                current.NoiDung = temp.Replace("\r\n", "")
                                .Replace("style=\"", "style='")
                                .Replace("href=\"", "href='")
                                .Replace("\">", "'>")
                                .Replace(";\"", ";'")
                                .Replace("\"", "&quot;");
                temp = HttpUtility.HtmlDecode(current.TieuDe).Replace("\r\n", "");
                current.TieuDe = temp;
            }
            return current;
        }

        public static List<THONGBAO> GetThongBaoTheoSoTrang(int soTrang, int soDongMoiTrang)
        {
            var skipRow = (soTrang - 1) * soDongMoiTrang;
            var list = GetAllThongBao().Skip(skipRow).Take(soDongMoiTrang).ToList();
            return list;
        }

        public static void AddThongBao(THONGBAO thongBao)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            db.THONGBAOs.Add(thongBao);
            db.SaveChanges();
        }

        public static long GetSoDong()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.THONGBAOs.Count();
        }

        public static int GetMaxId()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.THONGBAOs.Max(t => t.MaThongBao);
        }
    }
}