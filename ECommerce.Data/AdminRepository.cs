using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class AdminRepository
    {
        private string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(string firstName, string lastName, string emailAddress, string password)
        {
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);

            using (ECommerceDbDataContext context = new ECommerceDbDataContext(_connectionString))
            {
                AdminUser user = new AdminUser
                {
                    Email = emailAddress,
                    FirstName = firstName,
                    LastName = lastName,
                    PasswordHash = hash,
                    Salt = salt
                };
                context.AdminUsers.InsertOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public AdminUser Login(string emailAddress, string password)
        {
            AdminUser user = GetUser(emailAddress);
            if (user == null)
            {
                return null;
            }

            bool isMatch = PasswordHelper.PasswordMatch(password, user.Salt, user.PasswordHash);
            if (isMatch)
            {
                return user;
            }

            return null;
        }

        public AdminUser GetUser(string emailAddress)
        {
            using (ECommerceDbDataContext context = new ECommerceDbDataContext(_connectionString))
            {
                return context.AdminUsers.FirstOrDefault(u => u.Email == emailAddress);
            }
        }

        public void AddCategory(Category category)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                context.Categories.InsertOnSubmit(category);
                context.SubmitChanges();
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

        public void AddProduct(Product product)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                context.Products.InsertOnSubmit(product);
                context.SubmitChanges();
            }
        }

        public Category GetCategory(string name)
        {
            using (var context = new ECommerceDbDataContext(_connectionString))
            {
                return context.Categories.FirstOrDefault(c => c.Name == name);
            }
        }
    }
}
