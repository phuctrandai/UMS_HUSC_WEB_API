using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.Daos
{
    public static class FireBaseDao
    {
        public static bool AddFireBase(string maSinhVien, string token)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                try
                {
                    var current = db.FIREBASEs.FirstOrDefault(f => f.MaSinhVien.Equals(maSinhVien) && f.Token.Equals(token));
                    if (current != null)
                        return false;

                    FIREBASE fIREBASE = new FIREBASE() { Id = 1, MaSinhVien = maSinhVien, Token = token };
                    db.FIREBASEs.Add(fIREBASE);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool DeleteFireBase(string maSinhVien, string token)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                try
                {
                    var current = db.FIREBASEs.FirstOrDefault(f => f.MaSinhVien.Equals(maSinhVien) && f.Token.Equals(token));
                    if (current != null)
                        db.FIREBASEs.Remove(current);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static FIREBASE GetFireBase(string maSinhVien)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.FIREBASEs.FirstOrDefault(f => f.MaSinhVien.Equals(maSinhVien));
        }

        public static List<FIREBASE> GetAllFireBase()
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.FIREBASEs.ToList();
        }

        public static string[] GetFireBaseTokenById(List<int> idList)
        {
            using (UMS_HUSCEntities db = new UMS_HUSCEntities())
            {
                List<string> list = new List<string>();
                idList.ForEach(x =>
                {
                    var sinhVien = db.VThongTinCaNhans.FirstOrDefault(v => v.MaTaiKhoan == x);
                    if (sinhVien != null)
                    {
                        var fireBases = db.FIREBASEs.Where(f => f.MaSinhVien.Equals(sinhVien.MaSinhVien));
                        if (fireBases != null && fireBases.Count() > 0)
                            fireBases.ToList().ForEach(f => { list.Add(f.Token); });
                    }
                });
                return list.ToArray();
            }
        }

        public static FIREBASE GetFireBaseByToken(string token)
        {
            UMS_HUSCEntities db = new UMS_HUSCEntities();
            return db.FIREBASEs.FirstOrDefault(f => f.Token.Equals(token));
        }
    }
}