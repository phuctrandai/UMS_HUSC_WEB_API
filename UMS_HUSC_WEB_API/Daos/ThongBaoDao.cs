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
                x.NoiDung = temp.Replace("\"", "'").Replace("\r\n", "");
                Console.WriteLine(x.NoiDung);
            });

            return listThongBao;
        }

        public static void AddThongBao(THONGBAO thongBao)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            db.THONGBAOs.Add(thongBao);
            db.SaveChanges();
        }
    }
}