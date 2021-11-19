using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ThietBiDienTu.Models
{
    public class Producer
    {
        [Key]
        public int ProducerID { get; set; }
        [Required]
        public string ProducerName { get; set; }
        public string ProducerAddress { get; set; }
        public string ProducerEmail { get; set; }
        public int ProducerPhone { get; set; }
    }
}