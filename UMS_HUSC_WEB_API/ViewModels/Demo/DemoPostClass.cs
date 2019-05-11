using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.ViewModels.Demo
{
    public class DemoPostClass
    {
        public List<HOCPHAN> HocPhans { get; set; }
        public List<VHocKy> HocKys { get; set; }
        public List<GIANGVIEN> GiangViens { get; set; }
        public List<PHONGHOC> PhongHocs { get; set; }

        public DemoPostClass()
        {
        }
    }

    public class DemoSignUpCourse
    {
        public List<VHocKy> HocKys { get; set; }
        public List<LOPHOCPHAN> LopHocPhans { get; set; }
        public List<VThongTinChung> sinhViens { get; set; }
   
        public DemoSignUpCourse()
        {
        }
    }
}