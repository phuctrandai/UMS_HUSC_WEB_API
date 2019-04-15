using UMS_HUSC_WEB_API.Models;
using UMS_HUSC_WEB_API.ViewModels;

namespace UMS_HUSC_WEB_API.ViewModels
{
    public class VLyLichCaNhan
    {
        public ThongTinChung ThongTinChung { get; set; }
        public VThongTinLienHe ThongTinLienHe { get; set; }
        public VThuongTru ThuongTru { get; set; }
        public VQueQuan QueQuan { get; set; }
        public VDacDiemBanThan DacDiemBanThan { get; set; }
        public VLichSuBanThan LichSuBanThan { get; set; }
    }
}