using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.Controllers
{
    public class SinhVienController : ApiController
    {
        private UMS_HUSCEntities db = new UMS_HUSCEntities();

        // PUT: api/SinhVien/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSINHVIEN(string id, SINHVIEN sINHVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sINHVIEN.MaSinhVien)
            {
                return BadRequest();
            }

            db.Entry(sINHVIEN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SINHVIENExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SinhVien/Login
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PostLogin(string id, SINHVIEN sinhVien)
        {
            if (string.IsNullOrEmpty(id) || (sinhVien == null))
                return NotFound();

            if (!id.ToLower().Equals("login"))
                return NotFound();

            var current = db.SINHVIENs.FirstOrDefault(
                sv => sv.MaSinhVien.ToLower().Equals(sinhVien.MaSinhVien.ToLower())
                && sv.MatKhau.Equals(sinhVien.MatKhau));

            if (current == null)
                return NotFound();
            
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SINHVIENExists(string id)
        {
            return db.SINHVIENs.Count(e => e.MaSinhVien == id) > 0;
        }
    }
}