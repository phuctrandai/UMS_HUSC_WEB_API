using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;
using UMS_HUSC_WEB_API.ViewModels.Demo;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class DemoController : Controller
    {
        [HttpGet]
        public ActionResult DemoPostNews(DemoKetQua demo)
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

        [HttpGet]
        public ActionResult DemoPostMessage(DemoKetQua demo)
        {
            return View(demo);
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

        [HttpGet]
        public ActionResult DemoPostClass()
        {
            using (var db = new UMS_HUSCEntities())
            {
                var demo = new DemoPostClass()
                {

                    PhongHocs = db.PHONGHOCs.ToList(),
                    HocKys = db.VHocKies.OrderByDescending(i => i.NamBatDau).ToList(),
                    GiangViens = db.GIANGVIENs.ToList(),
                    HocPhans = db.HOCPHANs.ToList(),
                };
                return View(demo);
            }
        }

        [HttpPost]
        public ActionResult DemoPostClass(FormCollection form)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var hocKy = form["hocKy"];
                var hocPhan = form["hocPhan"];
                var giangVien = form["giangVien"];
                var ngayHetHanDangKy = form["ngayHetHanDangKy"];
                var ngayBatDauHoc = form["ngayBatDauHoc"];
                var ngayKetThucHoc = form["ngayKetThucHoc"];
                var phongHoc = form["phongHoc"];
                var tietHocBatDau = form["tietHocBatDau"];
                var tietHocKetThuc = form["tietHocKetThuc"];
                var ngayTrongTuan = form["ngayTrongTuan"];
                var soThuTu = form["soThuTu"];

                LopHocPhanDao.AddLopHocPhan(hocPhan, int.Parse(soThuTu), int.Parse(hocKy), int.Parse(giangVien), DateTime.Parse(ngayHetHanDangKy), DateTime.Parse(ngayBatDauHoc),
                    DateTime.Parse(ngayKetThucHoc), int.Parse(phongHoc), int.Parse(ngayTrongTuan), int.Parse(tietHocBatDau), int.Parse(tietHocKetThuc));

                return RedirectToAction("DemoPostClass", "Demo", null);
            }
        }

        [HttpGet]
        public ActionResult DemoSignUpClassFirst()
        {
            using (var db = new UMS_HUSCEntities())
            {
                var hocKys = db.VHocKies.OrderByDescending(i => i.TenHocKy)
                    .OrderByDescending(i => i.NamBatDau).ToList();
                return View(new DemoSignUpCourse() { HocKys = hocKys });
            }
        }

        [HttpGet]
        public ActionResult DemoSignUpClassSecond(int hocKy)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var lopHocPhans = db.LOPHOCPHANs.Where(i => i.HocKy == hocKy).ToList();
                var sinhViens = db.VThongTinChungs.ToList();
                return View(new DemoSignUpCourse() { LopHocPhans = lopHocPhans, sinhViens = sinhViens });
            }
        }

        [HttpPost]
        public ActionResult DemoSignUpClassThird(FormCollection form)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var maSinhVien = form["sinhVien"];
                var lopHocPhan = form["lopHocPhan"];
                LopHocPhanDao.DangKyLop(maSinhVien, lopHocPhan);
                return RedirectToAction("DemoSignUpClassFirst", "Demo");
            }
        }

        [HttpGet]
        public ActionResult DemoAddScheduleFirst(DemoKetQua demo)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var maxHocKy = db.VHocKies.Max(i => i.MaHocKy);
                var lopHocPhans = db.LOPHOCPHANs.Where(i => i.HocKy == maxHocKy).ToList();
                var phongHocs = db.PHONGHOCs.ToList();
                return View(new DemoAddSchedule() { LopHocPhans = lopHocPhans, PhongHocs = phongHocs, Demo = demo });
            }
        }

        [HttpPost]
        public ActionResult DemoAddScheduleSecond(FormCollection form)
        {
            var maLopHocPhan = form["maLopHocPhan"];
            var tietHocBatDau = form["tietHocBatDau"];
            var tietHocKetThuc = form["tietHocKetThuc"];
            var ngayHoc = form["ngayHoc"];
            var phongHoc = form["phongHoc"];

            var lichHoc = new LICHHOC()
            {
                MaLopHocPhan = maLopHocPhan,
                PhongHoc = int.Parse(phongHoc),
                TietHocBatDau = int.Parse(tietHocBatDau),
                TietHocKetThuc = int.Parse(tietHocKetThuc),
                NgayHoc = DateTime.ParseExact(ngayHoc, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };

            DemoKetQua demo = new DemoKetQua();

            if (LopHocPhanDao.TonTaiLichHoc(lichHoc))
            {
                demo.PhanHoi = "Lịch học đã tồn tại";
                return RedirectToAction("DemoAddScheduleFirst", "Demo", demo);
            }

            LopHocPhanDao.AddLichHoc(lichHoc);
            FCMController fcm = new FCMController();
            string notification = fcm.CreateScheduleNotification(lichHoc);
            string response = fcm.SendMessage(notification);

            demo.PhanHoi = response;
            demo.TinGuiDi = notification;

            return RedirectToAction("DemoAddScheduleFirst", "Demo", demo);
        }

        [HttpGet]
        public ActionResult DemoSubScheduleFirst(DemoKetQua demo)
        {
            using (var db = new UMS_HUSCEntities())
            {
                var maxHocKy = db.VHocKies.Max(i => i.MaHocKy);
                var lopHocPhans = db.LOPHOCPHANs.Where(i => i.HocKy == maxHocKy).ToList();
                return View(new DemoAddSchedule() { LopHocPhans = lopHocPhans, Demo = demo });
            }
        }
         
        [HttpPost]
        public ActionResult DemoSubScheduleSecond(FormCollection form)
        {
            var maLopHocPhan = form["maLopHocPhan"];
            var lichHocs = LopHocPhanDao.GetLichHoc(maLopHocPhan);
            return View(lichHocs);
        }
    }
}