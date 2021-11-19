using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Producer);
            return View(products.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            ViewBag.productID = id;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,CategoryID,ProducerID,Status,ProductQuantity,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", product.ProducerID);
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", product.ProducerID);
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,CategoryID,ProducerID,Status,ProductQuantity,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "ProducerName", product.ProducerID);
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ShowProduct()
        {
            var products = db.Products.Where(m => m.Status == "Available");
            return View(products.ToList());
        }

        public ActionResult ProductByName(string productName)
        {
            var name = productName;
            var products = db.Products.Where(m => m.ProductName.Contains(name));
            return View(products.ToList());
        }

        
        public ActionResult ProductByCategory(string productCategory)
        {
            var name = productCategory;
            var products = db.Products.Where(m => m.Category.CategoryName == name);
            return View(products.ToList());
        }
    }
}
