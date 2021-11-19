using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThietBiDienTu.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Descriptions { get; set; }

    }
}