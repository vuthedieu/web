using System;
using System.Collections.Generic;

namespace BTLWeb.Models;

public partial class TChiTietHdb
{
    public int Id { get; set; }

    public int MaHoaDon { get; set; }

    public string MaSp { get; set; } = null!;

    public int? SoLuongBan { get; set; }

    public decimal? DonGiaBan { get; set; }

    public double? GiamGia { get; set; }

    public string? GhiChu { get; set; }

    public virtual THoaDonBan MaHoaDonNavigation { get; set; } = null!;

    public virtual TDanhMucSp MaSpNavigation { get; set; } = null!;
}
