using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public ActionResult DemoPostNews(DemoKetQua demo)
        {
            return View(demo);
        }

        [HttpGet]
        public ActionResult DemoPostMessage(DemoKetQua demo)
        {
            return View(demo);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DemoPostNews(FormCollection form)
        {
            string tieuDe = HttpUtility.HtmlEncode(form["txtTieuDe"]);
            string noiDung = HttpUtility.HtmlEncode(form["txtNoiDung"]);
            DateTime thoiGianDang = DateTime.Now;

            // Them vao db
            THONGBAO thongBao = new THONGBAO()
            {
                MaThongBao = 1,
                TieuDe = tieuDe,
                NoiDung = noiDung,
                ThoiGianDang = thoiGianDang
            };

            DemoKetQua demo = new DemoKetQua();
            try
            {
                ThongBaoDao.AddThongBao(thongBao);
                thongBao.MaThongBao = ThongBaoDao.GetMaxId();

                // Thong bao den client app
                FCMController fcm = new FCMController();
                string message = fcm.CreateNewsNotification(thongBao);
                string response = fcm.SendMessage(message);

                // Hien thi ket qua thanh cong
                demo.KetQua = "Them thanh cong !!!";
                demo.PhanHoi = response;
                demo.TinGuiDi = message;
            }
            catch (Exception ex)
            {
                // hien thi loi
                demo.PhanHoi = ex.Message + ex.StackTrace;
            }

            return RedirectToAction("DemoPostNews", "Demo", demo);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DemoPostMessage(FormCollection form)
        {
            var demo = new DemoKetQua();

            var tieuDe = HttpUtility.HtmlEncode(form["txtTieuDe"]);
            var noiDung = HttpUtility.HtmlEncode(form["txtNoiDung"]);
            var maNguoiGui = form["txtMaNguoiNguoi"];
            var thoiDiemGui = DateTime.Now;
            var danhSachMaNguoiNhan = form["txtNguoiNhan[]"].Split(',').ToList<string>();

            try
            {
                // Them tin nhan vao db
                TINNHAN tinNhan = new TINNHAN()
                {
                    MaTinNhan = TinNhanDao.GetMaxMaTinNhan() + 1, // tu dong tang
                    TieuDe = tieuDe,
                    NoiDung = noiDung,
                    ThoiDiemGui = thoiDiemGui,
                };

                // Lay danh sach thong tin nguoi nhan
                var danhSachNguoiNhan = new List<NGUOINHAN>();
                danhSachMaNguoiNhan.ForEach(m => danhSachNguoiNhan.Add(
                    new NGUOINHAN()
                    {
                        MaTinNhan = tinNhan.MaTinNhan,
                        MaNguoiNhan = int.Parse(m),
                        TrangThai = TinNhanDao.TINNHAN_CHUA_XOA,
                        HoTenNguoiNhan = SinhVienDao.GetHoTenTheoTaiKhoan(int.Parse(m)),
                        ThoiDiemXem = null,
                        TINNHAN = tinNhan,
                        TAIKHOAN = null
                    }
                ));

                //// Nguoi gui
                var nguoiGui = new List<NGUOIGUI>
                {
                    new NGUOIGUI()
                    {
                        MaNguoiGui = int.Parse(maNguoiGui),
                        TrangThai = TinNhanDao.TINNHAN_CHUA_XOA,
                        MaTinNhan = tinNhan.MaTinNhan,
                        HoTenNguoiGui = SinhVienDao.GetHoTenTheoTaiKhoan(int.Parse(maNguoiGui)),
                        TINNHAN = tinNhan,
                        TAIKHOAN = null
                    }
                };

                tinNhan.NGUOINHANs = danhSachNguoiNhan;
                tinNhan.NGUOIGUIs = nguoiGui;
                TinNhanDao.AddTinNhan(tinNhan);

                // Thong bao den client app
                FCMController fcm = new FCMController();
                string message = fcm.CreateMessageNotification(tinNhan);
                demo.TinGuiDi = message;

                string response = fcm.SendMessage(message);
                demo.PhanHoi = response;

                // Hien thi ket qua thanh cong
                demo.KetQua = "Them thanh cong !!!";
            }
            catch (Exception ex)
            {
                demo.PhanHoi = ex.Message + ex.StackTrace;
            }

            return RedirectToAction("DemoPostMessage", "Demo", demo);
        }

        [HttpPost]
        public ActionResult DemoSelect2Search(string q)
        {
            var list = new List<Select2Model>();

            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
            {
                list = SinhVienDao.GetMaTaiKhoanVaHoTen().Where(s => s.text.ToLower().Contains(q.ToLower().Trim())).ToList();
            }

            return Json(new { items = list }, JsonRequestBehavior.AllowGet);
        }
    }
}