using BTLWeb.Models;
using Microsoft.AspNetCore.Mvc;
namespace BTLWeb.ViewComponents
{
    public class HangSxMenuViewComponent : ViewComponent
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
		public HangSxMenuViewComponent() { }

		public IViewComponentResult Invoke()
		{
			var lst = db.THangSxes.ToList();
			return View(lst);
		}
	}
}
