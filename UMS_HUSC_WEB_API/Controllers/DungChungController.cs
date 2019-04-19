using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class DungChungController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string order, int refId = 0)
        {
            if (string.IsNullOrEmpty(order)) return NotFound();

            using(var db = new Models.UMS_HUSCEntities())
            {
                switch (order.ToLower())
                {
                    case "quocgia":
                        return Ok(db.QUOCGIAs.Select(q => new QuocGia() { MaQuocGia = q.MaQuocGia, TenQuocGia = q.TenQuocGia }).ToArray());

                    case "thanhpho":
                        return Ok(db.THANHPHOes.Where(t => t.MaQuocGia == refId)
                            .Select(t => new ThanhPho() { MaThanhPho = t.MaThanhPho, TenThanhPho = t.TenThanhPho }).ToArray());

                    case "quanhuyen":
                        return Ok(db.QUANHUYENs.Where(q => q.MaThanhPho == refId)
                            .Select(q => new QuanHuyen() { MaQuanHuyen = q.MaQuanHuyen, TenQuanHuyen = q.TenQuanHuyen}).ToArray());

                    case "phuongxa":
                        return Ok(db.PHUONGXAs.Where(p => p.MaQuanHuyen == refId)
                            .Select(p => new PhuongXa() { MaPhuongXa = p.MaPhuongXa, TenPhuongXa = p.TenPhuongXa }).ToArray());

                    case "dantoc":
                        return Ok(db.DANTOCs.Select(d => new DanToc() { MaDanToc = d.MaDanToc, TenDanToc = d.TenDanToc }).ToArray());

                    case "tongiao":
                        return Ok(db.TONGIAOs.Select(t => new TonGiao() { MaTonGiao = t.MaTonGiao, TenTonGiao = t.TenTonGiao }).ToArray());

                    case "kytucxa":
                        return Ok(db.KYTUCXAs.Select(k => new KyTucXa() { MaKyTucXa = k.MaKyTucXa, TenKyTucXa = k.TenKyTucXa }).ToArray());

                    default:
                        return NotFound();
                }
            }
        }
    }
}
