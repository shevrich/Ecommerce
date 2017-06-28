using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class ItemsRepository
    {
        private string _connectionString;

        public ItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public IEnumerable<Product> GetProducts(int? categoryId)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.CategoryId);
                context.LoadOptions = loadOptions;

                return context.Products.Where(p => p.CategoryId == categoryId).ToList();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Category>(c => c.Products);
                context.LoadOptions = loadOptions;

                return context.Categories.ToList();
            }
        }

        public Product GetProduct(int id)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        public int GetFirstCategoryId()
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Category>(c => c.Products);
                context.LoadOptions = loadOptions;
                return context.Categories.FirstOrDefault().Id;
            }
        }
        public IEnumerable<Product> GetFirstCategoryProducts()
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Product>(p => p.CategoryId);
                context.LoadOptions = loadOptions;

                return context.Products.Where(p => p.CategoryId == GetFirstCategoryId()).ToList();
            }
        }

        public int AddShoppingCart(ShoppingCart shoppingCart)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                context.ShoppingCarts.InsertOnSubmit(shoppingCart);
                context.SubmitChanges();

                int id = shoppingCart.Id;
                return id;
            }
        }
        
        public void AddItemToCart(ShoppingCartItem item)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                context.ShoppingCartItems.InsertOnSubmit(item);
                context.SubmitChanges();
            }
        }

        public int GetItemsCount(int cartId)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                return context.ShoppingCartItems.Count(i => i.CartId == cartId);
            }
        }

        public IEnumerable<ShoppingCartItem> GetProductsPerCart(int cartId)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<ShoppingCartItem>(s => s.Product);
                loadOptions.LoadWith<ShoppingCartItem>(s => s.CartId);
                context.LoadOptions = loadOptions;

                return context.ShoppingCartItems.Where(s => s.CartId == cartId).ToList();
            }
        }

        public void Delete(int cartId, int productId)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                var item = context.ShoppingCartItems.FirstOrDefault(i => i.CartId == cartId && i.ProductId == productId);
                context.ShoppingCartItems.DeleteOnSubmit(item);
                context.SubmitChanges();
            }
        }

        public void Edit(ShoppingCartItem item)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                ShoppingCartItem result = context.ShoppingCartItems.FirstOrDefault(i => i.CartId == item.CartId && i.ProductId == item.ProductId);
                result.Quantity = item.Quantity;
                context.SubmitChanges();

            }
        }
    }
}
