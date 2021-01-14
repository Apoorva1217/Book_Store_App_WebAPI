using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_id { get; set; }

        [Required]
        public string BookName { get; set; }

        public string BookImage { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long Quantity { get; set; }

        [Required]
        public long Price { get; set; }

        public bool AddedToCart { get; set; }
    }
}
