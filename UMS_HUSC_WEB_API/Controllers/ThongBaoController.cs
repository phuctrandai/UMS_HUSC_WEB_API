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
        private const string ORDER_NOI_DUNG_THONG_BAO = "noidung";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Controller run ok");
        }
        
        [HttpGet]
        public IHttpActionResult ChiTiet(string maSinhVien, string matKhau, int id)
        {
            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var sv = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);
            if (sv == false) return BadRequest("Thong tin sinh vien khong hop le !");

            var tb = ThongBaoDao.GetThongBaoTheoId(id);
            if (tb == null) return BadRequest("Mã thông báo không tồn tại !");

            return Ok(tb);
        }

        [HttpGet]
        public IHttpActionResult DanhSach(string maSinhVien, string matKhau, int soTrang, int soDong)
        {
            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau)
                || soTrang < 1 || soDong < 1)
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var current = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);
            if (current == false) return BadRequest("Thong tin sinh vien khong hop le !");

            var soDongTong = ThongBaoDao.GetSoDong();
            var temp = soDongTong % soDong;
            var subTongSoTrang = soDongTong / soDong;
            var tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
            if (soTrang > tongSoTrang) return Ok(new List<THONGBAO>());

            var listThongBao = ThongBaoDao.GetThongBaoTheoSoTrang(soTrang, soDong);
            return Ok(listThongBao);
        }
    }
}
