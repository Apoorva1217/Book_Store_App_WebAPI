﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class UserRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
