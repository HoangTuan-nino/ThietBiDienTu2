using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiDienTu.Models
{
    public class CartItem
    {
        public Product shoppingProduct { get; set; }
        public int shoppingQuantity { get; set; }
    }

    
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add (Product product, int quantity = 1)
        {
            var item = items.FirstOrDefault(s => s.shoppingProduct.ProductID == product.ProductID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    shoppingProduct = product,
                    shoppingQuantity = quantity
                });
            }
            else
            {
                item.shoppingQuantity += quantity;
            }
        }
        public void UpdateQuantityShopping(int id, int quantity)
        {
            var item = items.Find(s => s.shoppingProduct.ProductID == id);
            if (item != null)
            {
                item.shoppingQuantity = quantity;
            }
        }
        public double TotalMoney()
        {
            var total = items.Sum(s => s.shoppingProduct.ProductPrice * s.shoppingQuantity);
            return total;
        }

        public void RemoveCartItem(int id)
        {
            items.RemoveAll(s => s.shoppingProduct.ProductID == id);
        }

        //Tong so luong mua
        public int TotalQuantityInCart()
        {
            return items.Sum(s => s.shoppingQuantity);
        }

        //Xoa gio hang de order moi
        public void ClearCart()
        {
            items.Clear();
        }
    }
}