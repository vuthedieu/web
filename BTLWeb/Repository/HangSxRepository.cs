using BTLWeb.Models;

namespace BTLWeb.Repository
{
    public class HangSxRepository : IHangSxRepository
    {
        private readonly QlbanMayAnhContext _context;
        public HangSxRepository(QlbanMayAnhContext context)
        {
            _context = context;
        }
        public THangSx Add(THangSx hangSx)
        {
            _context.THangSxes.Add(hangSx);
            _context.SaveChanges();
            return hangSx;
        }

        public THangSx Delete(string mahangSx)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<THangSx> GetAllhangSx()
        {
            return _context.THangSxes;
        }

        public THangSx GetLoaiSp(string mahangSx)
        {
            return _context.THangSxes.Find(mahangSx);
        }

        public THangSx Update(THangSx hangSx)
        {
            _context.THangSxes.Update(hangSx);
            _context.SaveChanges();
            return hangSx;
        }
    }
}
