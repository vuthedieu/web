using System;
using System.Collections.Generic;

namespace BTLWeb.Models;

public partial class THoaDonBan
{
    public int MaHoaDon { get; set; }

    public string? NgayHoaDon { get; set; }

    public string? MaKhachHang { get; set; }

    public decimal? TongTienHd { get; set; }

    public double? GiamGiaHd { get; set; }

    public byte? PhuongThucThanhToan { get; set; }

    public string? MaSoThue { get; set; }

    public string? ThongTinThue { get; set; }

    public string? GhiChu { get; set; }

    public int? Status { get; set; }

    public virtual TKhachHang? MaKhachHangNavigation { get; set; }

    public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; } = new List<TChiTietHdb>();
}
