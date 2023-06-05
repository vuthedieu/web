using BTLWeb.Models;

namespace BTLWeb.Repository
{
    public class HoaDonBanRepository : IHoaDonBanRepository
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();

        public THoaDonBan Add(THoaDonBan entity)
        {
            db.THoaDonBans.Add(entity);
            db.SaveChanges();
            return entity;
        }
    }
}
