using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS_HUSC_WEB_API.Models;

namespace UMS_HUSC_WEB_API.ViewModels.Demo
{
    public class DemoAddSchedule
    {
        public DemoKetQua Demo { get; set; }
        public List<LOPHOCPHAN> LopHocPhans { get; set; }
        public List<PHONGHOC> PhongHocs { get; set; }
    }
}