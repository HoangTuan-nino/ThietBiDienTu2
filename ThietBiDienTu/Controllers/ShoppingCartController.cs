using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Models;

namespace ThietBiDienTu.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        //Them vao gio hang
        public ActionResult AddToCart(int id)
        {
            var product = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (product != null)
            {
                GetCart().Add(product);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        //Trang hien thi gio hang
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public PartialViewResult ShowToCartNavBar()
        {
            Cart cart = Session["Cart"] as Cart;
            return PartialView(cart);
        }

        public ActionResult UpdateQuantityCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.UpdateQuantityShopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveCartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int totalItem = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                totalItem = cart.TotalQuantityInCart();
            ViewBag.QuantityCart = totalItem;
            return PartialView("BagCart");
        }

        public ActionResult ShoppingSuccess()
        {
            return View();
        }

        //Checkout
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.UserId = User.Identity.GetUserId();
                order.Descriptions = form["DeliveryAddress"];
                db.Orders.Add(order);

                foreach (var item in cart.Items)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderID = order.OrderID;
                    orderDetail.ProductID = item.shoppingProduct.ProductID;
                    orderDetail.UnitPriceSale = item.shoppingProduct.ProductPrice;
                    orderDetail.QuantitySale = item.shoppingQuantity;
                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("ShoppingSuccess", "ShoppingCart");
            }
            catch
            {
                return Content("Ops. Something wrong happened, please check your customer informations");
            }
        }
    }
}