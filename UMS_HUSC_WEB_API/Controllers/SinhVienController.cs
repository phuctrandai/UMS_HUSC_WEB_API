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
        private const string ORDER_GET_RECEIVED_MESSAGE = "tinnhandanhan";
        private const string ORDER_GET_SENT_MESSAGE = "tinnhandagui";
        private const string ORDER_GET_DELETED_MESSAGE = "tinnhandaxoa";
        private const string ORDER_GET_BODY_MESSAGE = "noidungtinnhan";

        // POST: api/SinhVien/order=...?masinhvien=...&matkhau...
        [HttpPost]
        public IHttpActionResult Post(string order, string maSinhVien, string matKhau)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !!!");

            if (!string.IsNullOrEmpty(order))
            {
                // Kiem tra thong tin dang nhap
                var current = SinhVienDao.GetSinhVien(maSinhVien, matKhau);

                if (current == null) return BadRequest("Thông tin sinh viên không đúng !!!");

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

        [HttpGet]
        public IHttpActionResult GetTinNhanDaNhanTheoTrang(string order, string maSinhVien, string matKhau, int soTrang, int soDongMoiTrang)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau)
                || soTrang < 1 || soDongMoiTrang < 1)
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var current = SinhVienDao.GetSinhVien(maSinhVien, matKhau);
            if (current == null) return BadRequest("Thong tin sinh vien khong hop le !");

            var listTinNhan = new List<TINNHAN>();

            switch(order.ToLower())
            {
                case ORDER_GET_RECEIVED_MESSAGE:
                    var soDong = TinNhanDao.GetTongTinNhanDaNhan(maSinhVien);
                    var temp = soDong % soDongMoiTrang;
                    var subTongSoTrang = soDong / soDongMoiTrang;
                    var tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                        listTinNhan = TinNhanDao.GetTinNhanDaNhanTheoSoTrang(maSinhVien, soTrang, soDongMoiTrang);
                    break;

                case ORDER_GET_SENT_MESSAGE:
                    soDong = TinNhanDao.GetTongTinNhanDaGui(maSinhVien);
                    temp = soDong % soDongMoiTrang;
                    subTongSoTrang = soDong / soDongMoiTrang;
                    tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                        listTinNhan = TinNhanDao.GetTinNhanDaGuiTheoSoTrang(maSinhVien, soTrang, soDongMoiTrang);
                    break;

                default:
                    break;
            }
            return Ok(listTinNhan);
        }

        [HttpGet]
        public IHttpActionResult GetTinNhanTheoId(string order, string maSinhVien, string matKhau, int id)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            if (!order.ToLower().Equals(ORDER_GET_BODY_MESSAGE))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var sv = SinhVienDao.GetSinhVien(maSinhVien, matKhau);
            if (sv == null) return BadRequest("Thông tin sinh viên không hợp lệ !");

            var tn = TinNhanDao.GetTinNhanTheoId(id, maSinhVien);
            if (tn == null) return BadRequest("Mã tin nhắn không tồn tại !");

            return Ok(tn);
        }
    }
}