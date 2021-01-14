using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class NewOrderProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public int Product_id { get; set; }

        [Required]
        public string Product_name { get; set; }

        [Required]
        public long Product_quantity { get; set; }

        [Required]
        public long Product_price { get; set; }
    }
}
