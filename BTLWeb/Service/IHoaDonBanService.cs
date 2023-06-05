using BTLWeb.Models;

namespace BTLWeb.Service
{
    public interface IHoaDonBanService
    {
        bool Create(THoaDonBan hoaDonBan, List<TChiTietHdb> chiTietHdbs);
    }
}
