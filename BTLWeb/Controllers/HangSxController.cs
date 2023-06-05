using BTLWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HangSxController : ControllerBase
	{

        QlbanMayAnhContext db = new QlbanMayAnhContext();
		[Route("{mahangSx}")]
		public List<TDanhMucSp> GetAllProductByBrand(string mahangSx)
		{
			var lst = db.TDanhMucSps.Where(x => x.MaHangSx == mahangSx).OrderBy(x => x.TenSp).ToList();
			return lst;
		}
	}
}
