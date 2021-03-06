﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class SinhVienController : ApiController
    {
        private const string ORDER_LOGIN = "dangnhap";
        private const string ORDER_CHANGE_PASS = "doimatkhau";
        private const string ORDER_GET_CV = "lylichcanhan";
        private const string ORDER_GET_RECEIVED_MESSAGE = "danhan";
        private const string ORDER_GET_SENT_MESSAGE = "dagui";
        private const string ORDER_GET_DELETED_MESSAGE = "daxoa";
        private const string ORDER_GET_BODY_MESSAGE = "noidung";
        private const string ORDER_ATTEMP_DELETE_MESSAGE = "xoatamthoi";
        private const string ORDER_UPDATE_SEEN_TIME = "capnhatthoidiemxem";
        private const string ORDER_FOREVER_DELETE_MESSAGE = "xoavinhvien";
        private const string ORDER_RESTORE_MESSAGE = "khoiphuc";
        private const string ORDER_SEARCH_RECEIVED_MESSAGE = "timkiemdanhan";
        private const string ORDER_SEARCH_SENT_MESSAGE = "timkiemdagui";
        private const string ORDER_SEARCH_DELETED_MESSAGE = "timkiemdaxoa";

        // GET: api/SinhVien/[Action]/[order]?...
        [HttpGet]
        public IHttpActionResult TaiKhoan(string order, string maSinhVien, string matKhau, string matKhauMoi = "")
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !!!");

            if (!string.IsNullOrEmpty(order))
            {
                // Kiem tra thong tin dang nhap
                var current = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);

                if (current == false) return BadRequest("Thông tin sinh viên không đúng !!!");

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

                    case ORDER_CHANGE_PASS:
                        var result = SinhVienDao.DoiMatKhau(maSinhVien, matKhau, matKhauMoi);
                        if (result)
                            return Ok();

                        return BadRequest("Đổi mật khẩu không thành công");

                    default:
                        break;
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult TaiKhoan(string order, int soTrang = 1, int soDong = 1)
        {
            if (string.IsNullOrEmpty(order))
                return Ok();
            else
                return Ok(SinhVienDao.GetMaTaiKhoanVaHoTen(order, soTrang, soDong));
        }

        [HttpGet]
        public IHttpActionResult TinNhan(string order, string maSinhVien, string matKhau, int soTrang = 0, int soDong = 1)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var current = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);
            if (current == false)
                return BadRequest("Thông tin sinh viên không hợp lệ !");

            var listTinNhan = new List<TINNHAN>();

            switch (order.ToLower())
            {
                case ORDER_GET_RECEIVED_MESSAGE:
                    var soDongTong = TinNhanDao.GetTongTinNhanDaNhan(maSinhVien);
                    var temp = soDongTong % soDong;
                    var subTongSoTrang = soDongTong / soDong;
                    var tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.GetTinNhanDaNhanTheoSoTrang(maSinhVien, soTrang, soDong);
                        return Ok(list);
                    }

                case ORDER_GET_SENT_MESSAGE:
                    soDongTong = TinNhanDao.GetTongTinNhanDaGui(maSinhVien);
                    temp = soDongTong % soDong;
                    subTongSoTrang = soDongTong / soDong;
                    tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.GetTinNhanDaGuiTheoSoTrang(maSinhVien, soTrang, soDong);
                        return Ok(list);
                    }

                case ORDER_GET_DELETED_MESSAGE:
                    soDongTong = TinNhanDao.GetTongTinNhanDaXoa(maSinhVien);
                    temp = soDongTong % soDong;
                    subTongSoTrang = soDongTong / soDong;
                    tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.GetTinNhanDaXoaTheoSoTrang(maSinhVien, soTrang, soDong);
                        return Ok(list);
                    }

                default:
                    break;
            }
            return Ok(listTinNhan);
        }

        [HttpGet]
        public IHttpActionResult TinNhan(string order, string maSinhVien, string matKhau, int id)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var sv = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);
            if (sv == false) return BadRequest("Thông tin sinh viên không hợp lệ !");

            switch (order.ToLower())
            {
                case ORDER_GET_BODY_MESSAGE:
                    var ud = TinNhanDao.UpdateThoiDiemXem(id, maSinhVien);
                    var tn = TinNhanDao.GetTinNhanTheoId(id);

                    if (tn == null || ud == false)
                        return BadRequest("Mã tin nhắn không tồn tại !");
                    else
                        return Ok(tn);

                case ORDER_ATTEMP_DELETE_MESSAGE:
                    var result = TinNhanDao.AttempDeleteTinNhan(id, maSinhVien);
                    if (result) return Ok();

                    return BadRequest("Chuyển vào thùng rác không thành công, kiểm tra lại mã tin nhắn !");

                case ORDER_FOREVER_DELETE_MESSAGE:
                    var resultDelete = TinNhanDao.ForeverDelete(id, maSinhVien);
                    if (resultDelete) return Ok();

                    return BadRequest("Xóa không thành công, kiểm tra lại mã tin nhắn !");

                case ORDER_UPDATE_SEEN_TIME:
                    var resultUpdate = TinNhanDao.UpdateThoiDiemXem(id, maSinhVien);
                    if (resultUpdate) return Ok();

                    return BadRequest("Cập nhật thời điểm xem không thành công, kiểm tra lại mã tin nhắn và mã người nhận !");

                case ORDER_RESTORE_MESSAGE:
                    var resultRestore = TinNhanDao.RestoreTinNhan(id, maSinhVien);
                    if (resultRestore) return Ok();

                    return BadRequest("Khôi phục không thành công, kiểm tra lại mã tin nhắn !");

                default:
                    break;
            }
            return NotFound();
        }

        [HttpGet]
        public IHttpActionResult TinNhan(string order, string maSinhVien, string matKhau, string tuKhoa, int soTrang, int soDong)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Tham số truyền vào không hợp lệ !");

            var current = SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau);
            if (current == false)
                return BadRequest("Thông tin sinh viên không hợp lệ !");

            var listTinNhan = new List<TINNHAN>();

            switch(order.ToLower())
            {
                case ORDER_SEARCH_RECEIVED_MESSAGE:
                    var soDongTong = TinNhanDao.GetTongTinNhanDaNhan(maSinhVien);
                    var temp = soDongTong % soDong;
                    var subTongSoTrang = soDongTong / soDong;
                    var tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.SearchTinNhanDaNhan(maSinhVien, tuKhoa, soTrang, soDong);
                        return Ok(list);
                    }

                case ORDER_SEARCH_SENT_MESSAGE:
                    soDongTong = TinNhanDao.GetTongTinNhanDaGui(maSinhVien);
                    temp = soDongTong % soDong;
                    subTongSoTrang = soDongTong / soDong;
                    tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.SearchTinNhanDaGui(maSinhVien, tuKhoa, soTrang, soDong);
                        return Ok(list);
                    }

                case ORDER_SEARCH_DELETED_MESSAGE:
                    soDongTong = TinNhanDao.GetTongTinNhanDaXoa(maSinhVien);
                    temp = soDongTong % soDong;
                    subTongSoTrang = soDongTong / soDong;
                    tongSoTrang = temp == 0 ? subTongSoTrang : subTongSoTrang + 1;
                    if (soTrang > tongSoTrang)
                        return Ok(listTinNhan);
                    else
                    {
                        var list = TinNhanDao.SearchTinNhanDaXoa(maSinhVien, tuKhoa, soTrang, soDong);
                        return Ok(list);
                    }

                default: break;
            }

            return NotFound();
        }

        [HttpPost]
        public IHttpActionResult TraLoiTinNhan(string maSinhVien, string matKhau, TinNhan tinNhan)
        {
            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau))
                return BadRequest("Thông tin người gửi không đúng");
            
            var hoTenNguoiGui = tinNhan.HoTenNguoiGui;
            var maxMaTinNhan = TinNhanDao.GetMaxMaTinNhan() + 1;

            TINNHAN newTinNhan = new TINNHAN()
            {
                MaTinNhan = maxMaTinNhan,
                TieuDe = tinNhan.TieuDe,
                NoiDung = tinNhan.NoiDung,
                ThoiDiemGui = DateTime.Now
            };

            NGUOIGUI nguoiGui = new NGUOIGUI()
            {
                MaTinNhan = maxMaTinNhan,
                MaNguoiGui = tinNhan.MaNguoiGui,
                HoTenNguoiGui = hoTenNguoiGui,
                TINNHAN = newTinNhan,
                TrangThai = TinNhanDao.TINNHAN_CHUA_XOA,
                TAIKHOAN = null
            };

            var nguoiNhans = new List<NGUOINHAN>();

            foreach (var item in tinNhan.NguoiNhans)
            {
                NGUOINHAN nguoiNhan = new NGUOINHAN()
                {
                    MaTinNhan = maxMaTinNhan,
                    HoTenNguoiNhan = SinhVienDao.GetHoTenTheoTaiKhoan(item.MaNguoiNhan),
                    ThoiDiemXem = null,
                    TINNHAN = newTinNhan,
                    TAIKHOAN = null,
                    TrangThai = TinNhanDao.TINNHAN_CHUA_XOA,
                    MaNguoiNhan = item.MaNguoiNhan
                };
                nguoiNhans.Add(nguoiNhan);
            }

            newTinNhan.NGUOIGUIs = new List<NGUOIGUI> { nguoiGui };
            newTinNhan.NGUOINHANs = nguoiNhans;
            TinNhanDao.AddTinNhan(newTinNhan);

            try
            {
                // Thong bao den client app
                FCMController fcm = new FCMController();
                string message = fcm.CreateMessageNotification(newTinNhan);
                string response = fcm.SendMessage(message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateThongTinChung(string maSinhVien, string matKhau, ThongTinChung thongTinChung)
        {
            if (thongTinChung == null || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Thông tin người dùng không được rỗng");

            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau) || (!maSinhVien.ToLower().Equals(thongTinChung.MaSinhVien.ToLower())))
                return BadRequest("Thông tin người dùng không đúng");

            if (SinhVienDao.UpdateThongTinChung(thongTinChung))
                return Ok(SinhVienDao.GetThongTinChung(maSinhVien));

            return BadRequest("Cập nhật không thành công");
        }

        [HttpPost]
        public IHttpActionResult UpdateThongTinLienHe(string maSinhVien, string matKhau, VLyLichCaNhan lyLichCaNhan)
        {
            if (lyLichCaNhan == null || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Thông tin người dùng không được rỗng");

            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau))
                return BadRequest("Thông tin người dùng không đúng");

            SinhVienDao.UpDateThongTinLienHe(maSinhVien, lyLichCaNhan.ThongTinLienHe);
            SinhVienDao.UpdateThongTinThuongTru(maSinhVien, lyLichCaNhan.ThuongTru);
            SinhVienDao.UpdateQueQuan(maSinhVien, lyLichCaNhan.QueQuan);
            return Ok(SinhVienDao.GetLyLichCaNhan(maSinhVien));
        }
        
        [HttpPost]
        public IHttpActionResult UpdateDacDiemBanThan(string maSinhVien, string matKhau, VDacDiemBanThan dacDiemBanThan)
        {
            if (dacDiemBanThan == null || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Thông tin người dùng không được rỗng");

            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau))
                return BadRequest("Thông tin người dùng không đúng");

            SinhVienDao.UpdateDacDiemBanThan(dacDiemBanThan);
            return Ok(SinhVienDao.GetDacDiemBanThan(maSinhVien));
        }

        [HttpGet]
        public IHttpActionResult HocKy(string order, string maSinhVien, string matKhau, int maHocKy = 0)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Thông tin người dùng không được rỗng");

            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau))
                return BadRequest("Thông tin người dùng không đúng");

            using (var db = new UMS_HUSCEntities())
            {
                switch (order.ToLower())
                {
                    case "danhsach":
                        var hocKys = db.VHocKies.OrderByDescending(i => i.MaHocKy).ToArray();
                        return Ok(hocKys);

                    case "tacnghiep":
                        var hocKy = db.VHocKies.FirstOrDefault(i => i.MaHocKy == maHocKy);
                        if (hocKy != null)
                        {
                            var sinhVien = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien.Equals(maSinhVien));
                            if (sinhVien != null)
                            {
                                sinhVien.HocKyTacNghiep = hocKy.MaHocKy;
                                db.SaveChanges();
                                return ThoiKhoaBieu(maSinhVien, matKhau, hocKy.MaHocKy);
                            }
                        }
                        return BadRequest("Mã học kỳ không tồn tại");

                    case "dongbo":
                        var hocKyHienTai = db.SINHVIENs.FirstOrDefault(s => s.MaSinhVien.Equals(maSinhVien)).HocKyTacNghiep;
                        if (maHocKy == hocKyHienTai)
                            return Ok(true);

                        else
                            return Ok(hocKyHienTai);

                    default:
                        return BadRequest("Tham số không hợp lệ");
                }
            }
        }

        [HttpGet]
        public IHttpActionResult ThoiKhoaBieu(string maSinhVien, string matKhau, int maHocKy = 0)
        {
            if (string.IsNullOrEmpty(maSinhVien) || string.IsNullOrEmpty(matKhau))
                return BadRequest("Thông tin người dùng không được rỗng");

            if (!SinhVienDao.TonTaiSinhVien(maSinhVien, matKhau))
                return BadRequest("Thông tin người dùng không đúng");

            var list = SinhVienDao.GetThoiKhoaBieu(maSinhVien, maHocKy);

            return Ok(list);
        }
    }
}