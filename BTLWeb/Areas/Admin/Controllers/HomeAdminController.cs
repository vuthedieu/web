using BTLWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using X.PagedList;

namespace WebProject02.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HomeAdmin")]
    [Route("Admin")]

    public class HomeAdminController : Controller
    {
        QlbanMayAnhContext db = new QlbanMayAnhContext();
        [Route("")]
        [Route("Index")]

        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstSanPham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> pageList = new PagedList<TDanhMucSp>(lstSanPham, pageNumber, pageSize);
            return View(pageList);
        }
        [Route("DanhMucHoaDon")]
        public IActionResult DanhMucHoaDon(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lst = db.THoaDonBans.AsNoTracking().OrderBy(x => x.MaHoaDon);
            PagedList<THoaDonBan> pageList = new PagedList<THoaDonBan>(lst, pageNumber, pageSize);
            return View(pageList);
        }

        [Route("ThemSanPham")]
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }

        [Route("ThemSanPham")]
        [ValidateAntiForgeryToken] //Để nhập đúng sản phẩm
        [HttpPost]
        public IActionResult ThemSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
                /*              return RedirectToAction("DanhMucSanPham");
								return View("/Admin/HomeAdmin/DanhMucSanPham?page=1");
				*/
            }
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(String maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [ValidateAntiForgeryToken] //Để nhập đúng sản phẩm
        [HttpPost]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
                /*              return RedirectToAction("DanhMucSanPham");
								return View("/Admin/HomeAdmin/DanhMucSanPham?page=1");
				*/
            }
            return View(sanPham);
        }

        [Route("XemChiTietSP")]
        [HttpGet]
        public IActionResult XemChiTietSP(String maSanPham)
        {

            TDanhMucSp sanPham = db.TDanhMucSps.Find(maSanPham);
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            return View(sanPham);
        }
        [Route("XemChiTietHD")]
        [HttpGet]
        public IActionResult XemChiTietHD(int mahoadon)
        {

            ViewBag.MaHDB = mahoadon;
            var lst = db.TChiTietHdbs.Where(x => x.MaHoaDon == mahoadon).AsNoTracking();
            return View(lst);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(String maSanPham)
        {
            TempData["Message"] = "";
            var listChiTiet = db.TDanhMucSps.Where(x => x.MaSp == maSanPham);
            foreach (var item in listChiTiet)
            {
                if (db.TChiTietHdbs.Where(x => x.MaSp == item.MaSp) != null)
                {
                    TempData["Message"] = "Không xóa được sản phẩm này!";
                    return RedirectToAction("DanhMucSanPham");
                }
            }
            var listAnh = db.TAnhSps.Where(x => x.MaSp == maSanPham);
            if (listAnh != null) db.RemoveRange(listAnh);
            if (listChiTiet != null) db.RemoveRange(listChiTiet);
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham");
        }


        [Route("ThongKe")]
        [HttpGet]
        public IActionResult ThongKe(int? thang, int? nam)
        {
            List<THoaDonBan> lstHoaDon = db.THoaDonBans.FromSqlRaw("SELECT * FROM THoaDonBan WHERE YEAR(CONVERT(datetime, NgayHoaDon, 101)) = {0} and month(CONVERT(datetime, NgayHoaDon, 101)) = {1}", nam, thang).ToList();
            var sum = 0;
            foreach (var item in lstHoaDon)
            {
                sum += (int) item.TongTienHd;
            }
            return View(sum);
        }
    }
}
