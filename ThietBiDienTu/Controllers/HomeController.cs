using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<Product> hot = new List<Product>();
            List<Product> laptop = new List<Product>();
            List<Product> banPhim = new List<Product>();

            hot = db.Products.Where(p => p.Status == "Hot").ToList();
            laptop = db.Products.Where(p => p.Category.CategoryName == "Laptop").ToList();
            banPhim = db.Products.Where(p => p.Category.CategoryName == "Bàn phím").ToList();

            ViewBag.Hot = hot;
            ViewBag.Laptop = laptop;
            ViewBag.BanPhim = banPhim;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}