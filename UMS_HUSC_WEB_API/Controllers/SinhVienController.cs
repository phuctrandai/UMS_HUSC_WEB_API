using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class SinhVienController : ApiController
    {
        private const string ORDER_LOGIN = "dangnhap";
        private const string ORDER_GET_CV = "lylichcanhan";

        // POST: api/SinhVien/order=...?masinhvien=...&matkhau...
        [HttpPost]
        public IHttpActionResult Post(string order, string maSinhVien, string matKhau)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return NotFound();

            if (!string.IsNullOrEmpty(order))
            {
                var current = SinhVienDao.GetSinhVien(maSinhVien, matKhau);

                if (current == null) return NotFound();

                switch (order.ToLower())
                {
                    case ORDER_LOGIN:
                        var thongTinCaNhan = SinhVienDao.GetThongTinCaNhan(maSinhVien);
                        if (thongTinCaNhan != null)
                            return Ok(thongTinCaNhan);
                        break;
                    case ORDER_GET_CV:
                        var lyLich = SinhVienDao.GetLyLichCaNhan(maSinhVien);
                        if (lyLich != null)
                            return Ok(lyLich);
                        break;
                    default:
                        break;
                }
            }
            return NotFound();
        }
    }
}