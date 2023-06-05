using BTLWeb.Models;
using BTLWeb.Models.ViewModels;

namespace BTLWeb.Constants
{
    public static class EntityExtentions
    {
        public static void UpdateOrder(this THoaDonBan order, OrderViewModel orderVm)
        {
            order.NgayHoaDon = orderVm.NgayHoaDon;
            order.TongTienHd = (decimal?) orderVm.TongTienHD;
            order.MaKhachHang = orderVm.MaKhachHang;
            order.GhiChu = orderVm.GhiChu;
            order.PhuongThucThanhToan = orderVm.PhuongThucThanhToan;
            order.TongTienHd = orderVm.TongTienHD;
            order.Status = orderVm.TrangThai;
        }
    }
}
