using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThietBiDienTu.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Producer")]
        public int? ProducerID { get; set; }
        public virtual Producer Producer { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [Range(0,1000)]
        public int ProductQuantity { get; set; }
        [Required]
        [Range(0,300000000)]
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}