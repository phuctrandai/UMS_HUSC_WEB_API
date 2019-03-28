using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class ThongBaoController : ApiController
    {
        private const string ORDER_TAT_CA_THONG_BAO = "tatca";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Controller run ok");
        }

        [HttpGet]
        public IHttpActionResult Get(string order, string maSinhVien, string matKhau)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return NotFound();

            if (!string.IsNullOrEmpty(order))
            {
                var current = SinhVienDao.GetSinhVien(maSinhVien, matKhau);

                if (current == null) return NotFound();

                switch (order.ToLower())
                {
                    case ORDER_TAT_CA_THONG_BAO: // Load tat ca thong bao moi nhat
                        var listThongBao = ThongBaoDao.GetAllThongBao();
                        return Ok(listThongBao);

                    default:
                        break;
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult GetThongBaoTheoSoTrang(string maSinhVien, string matKhau, int soTrang, int soDongMoiTrang)
        {
            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau)
                || soTrang < 1 || soDongMoiTrang < 1)
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var current = SinhVienDao.GetSinhVien(maSinhVien, matKhau);
            if (current == null) return BadRequest("Thong tin sinh vien khong hop le !");

            var temp = ThongBaoDao.GetSoDong() % soDongMoiTrang;
            var subTongSoTrang = ThongBaoDao.GetSoDong() / soDongMoiTrang;
            var tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
            if (soTrang > tongSoTrang) return Ok(new List<THONGBAO>());

            var listThongBao = ThongBaoDao.GetThongBaoTheoSoTrang(soTrang, soDongMoiTrang);
            return Ok(listThongBao);
        }
    }
}
