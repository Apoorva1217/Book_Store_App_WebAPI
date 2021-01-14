using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<AdminUserRegistration> adminUserRegistrations { get; set; }
        public DbSet<UserRegistration> userRegistrations { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<NewOrder> newOrders { get; set; }
        public DbSet<CustomerDetails> customerDetails { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
    }
}
