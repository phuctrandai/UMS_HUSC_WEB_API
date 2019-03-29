using System;
using System.Web;
using UMS_HUSC_WEB_API.Controllers;
using UMS_HUSC_WEB_API.Daos;
using UMS_HUSC_WEB_API.Models;

namespace DemoWebsite
{
    public partial class DemoPostNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGui_Click(object sender, EventArgs e)
        {
            string tieuDe = HttpUtility.HtmlEncode(txtTieuDe.Text);
            string noiDung = HttpUtility.HtmlEncode(txtNoiDung.Text);
            DateTime thoiGianDang = DateTime.Now;

            // Them vao db
            THONGBAO thongBao = new THONGBAO()
            {
                MaThongBao = 1,
                TieuDe = tieuDe,
                NoiDung = noiDung,
                ThoiGianDang = thoiGianDang
            };

            try
            {
                ThongBaoDao.AddThongBao(thongBao);
                thongBao.MaThongBao = ThongBaoDao.GetMaxId();

                // Thong bao den client app
                FCMController fcm = new FCMController();
                string message = fcm.CreateNewsNotification(thongBao);
                string response = fcm.SendMessage(message);

                // Hien thi ket qua thanh cong
                lblKetQua.Text = "Them thanh cong !!!";
                lblKetQua.CssClass = "text-success";
                lblLoi.Text = response;
                lblTinGuiDi.Text = message;
            }
            catch (Exception ex)
            {
                // hien thi loi
                lblLoi.Text = ex.Message + ex.StackTrace;
                return;
            }
        }
    }
}