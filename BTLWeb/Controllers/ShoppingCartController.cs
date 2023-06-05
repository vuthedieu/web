using BTLWeb.Constants;
using BTLWeb.Models;
using BTLWeb.Models.ViewModels;
using BTLWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Text.Json;

namespace BTLWeb.Controllers
{
    public class ShoppingCartController : Controller
    {   
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        HoaDonBanService hoaDonBanService = new HoaDonBanService();

        public ActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            return View(cart);
        }

        [HttpPost]
        public JsonResult Add(string productId)
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = db.TDanhMucSps.Find(productId);
                newItem.Product = product;
                newItem.Quantity = 1;
                cart.Add(newItem);
            }

            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, cart);

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            return Json(cart);
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if(item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }

            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, cartSession);
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {

            HttpContext.Session.Set<List<ShoppingCartViewModel>>(CommonConstants.SessionCart, new List<ShoppingCartViewModel>());
            return Json(new
            {
                status = true
            });
        }

        public JsonResult CheckKhachHang()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username != null)
            {
                var result = db.TKhachHangs.Find(username);
                if(result != null)
                {
                    return Json(new
                    {
                        status = true
                    });
                }
            }
            return Json(new
            {
                status = false
            });
        }

        public JsonResult CreateOrderNoCreateKhachHang(string orderViewModel)
        {
            var username = HttpContext.Session.GetString("Username");

            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            order.NgayHoaDon = DateTime.Now.ToString();
            var orderNew = new THoaDonBan();

            orderNew.UpdateOrder(order);
            orderNew.MaKhachHang = username;
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if (cart == null || cart.Count() == 0)
            {
                return Json(new
                {
                    status = false
                });
            }
            List<TChiTietHdb> orderDetails = new List<TChiTietHdb>();

            foreach (var item in cart)
            {
                var detail = new TChiTietHdb();
                detail.MaSp = item.ProductId;
                detail.SoLuongBan = item.Quantity;
                detail.DonGiaBan = item.Product.GiaLonNhat;
                orderDetails.Add(detail);
            }

            if (hoaDonBanService.Create(orderNew, orderDetails) == true)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public JsonResult CreateOrder(string orderViewModel, string khachHang)
        {
            TKhachHang _khachHang = new JavaScriptSerializer().Deserialize<TKhachHang>(khachHang);
            if (HttpContext.Session.GetString("Username") != null)
            {
                _khachHang.Username = HttpContext.Session.GetString("Username");
                _khachHang.MaKhanhHang = HttpContext.Session.GetString("Username");
                db.TKhachHangs.Add(_khachHang);
                db.SaveChanges();
            }

            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            order.NgayHoaDon = DateTime.Now.ToString();
            var orderNew = new THoaDonBan();

            orderNew.UpdateOrder(order);
            orderNew.MaKhachHang = _khachHang.MaKhanhHang;
            var cart = HttpContext.Session.Get<List<ShoppingCartViewModel>>(CommonConstants.SessionCart);
            if(cart == null || cart.Count() == 0)
            {
                return Json(new
                {
                    status = false
                });
            }
            List<TChiTietHdb> orderDetails = new List<TChiTietHdb>();
             
            foreach (var item in cart)
            {
                var detail = new TChiTietHdb();
                detail.MaSp = item.ProductId;
                detail.SoLuongBan = item.Quantity;
                detail.DonGiaBan = item.Product.GiaLonNhat;
                orderDetails.Add(detail);
            }

            if(hoaDonBanService.Create(orderNew, orderDetails) == true)
            {
                return Json(new
                    {
                        status = true
                    });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
