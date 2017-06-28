using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data;

namespace ECommerce.Models
{
    public class ItemsViewModel
    {
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }
        public Product product { get; set; }
        public int CartId { get; set; }
        public IEnumerable<ShoppingCartItem> ItemsPerCart { get; set; }
        public string message { get; set; }
    }
}