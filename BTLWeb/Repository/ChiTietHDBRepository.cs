using BTLWeb.Models;

namespace BTLWeb.Repository
{
    public class ChiTietHDBRepository : IChiTietHDBRepository
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        public TChiTietHdb Add(TChiTietHdb entity)
        {
            db.TChiTietHdbs.Add(entity);
            db.SaveChanges();
            return null;
        }
    }
}
