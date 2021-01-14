using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItem_id { get; set; }
        public int Product_id { get; set; }

        [ForeignKey("Product_id")]
        public Product Product { get; set; }
        public string LoginUser { get; set; }
        public int QuantityToBuy { get; set; }
        public bool AddedToCart { get; set; }
        public long Price { get; set; }
    }
}
