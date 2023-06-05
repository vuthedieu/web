using System.ComponentModel.DataAnnotations;

namespace BTLWeb.Models.ViewModels
{
    public class OrderViewModel
    {
        public int MaHoaDon { set; get; }

        public string NgayHoaDon { set; get; }

        [MaxLength(128)]
        public string MaKhachHang { set; get; }

        public decimal TongTienHD { set; get; }
        
        [MaxLength(256)]
        public byte PhuongThucThanhToan { set; get; }

        [Required]
        [MaxLength(256)]
        public string GhiChu { set; get; }

        public int TrangThai { set; get; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { set; get; }
    }
}
