using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [ForeignKey("Order")]
        public int? OrderID { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public double UnitPriceSale { get; set; }
        public double QuantitySale { get; set; }
    }
}