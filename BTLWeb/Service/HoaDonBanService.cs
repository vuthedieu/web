using BTLWeb.Models;
using BTLWeb.Repository;

namespace BTLWeb.Service
{
    public class HoaDonBanService : IHoaDonBanService
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        IHoaDonBanRepository hoaDonBanRepository = new HoaDonBanRepository();
        IChiTietHDBRepository chiTietHDBRepository = new ChiTietHDBRepository();
        
        public bool Create(THoaDonBan hoaDonBan, List<TChiTietHdb> chiTietHdbs)
        {
            try
            {
                int key = 1;
                if(db.THoaDonBans.Count() > 0)
                {
                    key = db.THoaDonBans.Max(x => x.MaHoaDon) + 1;
                }
                hoaDonBan.MaHoaDon = key;
                hoaDonBanRepository.Add(hoaDonBan);
                db.SaveChanges();
                foreach (var chiTietHDB in chiTietHdbs)
                {
                    chiTietHDB.MaHoaDon = key;
                    int otherKey = 1;
                    if (db.TChiTietHdbs.Count() > 0)
                    {
                        otherKey = db.TChiTietHdbs.Max(x => x.Id) + 1;
                    }
                    chiTietHDB.Id = otherKey;
                    chiTietHDBRepository.Add(chiTietHDB);
                }

                db.SaveChanges();
                return true;
            } catch(Exception ex)
            {
                return false;
            }

        }
    }
}
