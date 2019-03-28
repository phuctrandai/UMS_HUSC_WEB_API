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
            string tieuDe = txtTieuDe.Text;
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
            }
            catch (Exception ex)
            {
                // hien thi loi
                txtKetQua.Text = ex.Message + ex.StackTrace;
                return;
            }

            // Thong bao den client app
            //FCMController fcm = new FCMController();
            //string message = fcm.CreateNewsNotification(thongBao);
            //string response = fcm.SendMessage(message);
            //txtKetQua.Text = response;

            txtTieuDe.Text = "";
            txtNoiDung.Text = "";
        }
    }
}