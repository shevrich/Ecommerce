using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data;
using ECommerce.Models;
using System.Web.Security;

namespace ECommerce.Controllers
{
    [Cart]
    public class HomeController : Controller
    {
        public ActionResult Index(int? CategoryId)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            ItemsViewModel vm = new ItemsViewModel();
            vm.categories = repo.GetCategories();
            if (CategoryId == null)
            {
                vm.products = repo.GetFirstCategoryProducts();
            }
            else
            {
                vm.products = repo.GetProducts(CategoryId);
            }
            return View(vm);
        }
        public ActionResult Login()
        {
            AccountViewModel vm = new AccountViewModel();
            if(TempData.ContainsKey("TD"))
            {
                vm.message = (string)TempData["TD"];
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConStr);
            var user = repo.Login(email, password);
            if (user == null)
            {
                TempData["TD"] = "Wrong User Name or Password";
                return Redirect("/home/login");
            }
            FormsAuthentication.SetAuthCookie(email, true);
            return Redirect("/admin/index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        public ActionResult GetProducts(int categId)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            var result = repo.GetProducts(categId).Select(p => new
            {
                id = p.Id,
                categoryId = p.CategoryId,
                title = p.Title,
                description = p.Description,
                price = p.Price,
                imageFile = p.ImageFile,
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewImage(int Id)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            ItemsViewModel vm = new ItemsViewModel();
            vm.product = repo.GetProduct(Id);
            vm.categories = repo.GetCategories();
            if(TempData.ContainsKey("message"))
            {
                vm.message = (string)TempData["message"];
            }
            return View(vm);
        }

        [HttpPost]
        public void AddToCart(ShoppingCart cart, ShoppingCartItem item)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            if (Session["Cart"] == null)
            {
                Session["Cart"] = repo.AddShoppingCart(cart);
            }
            item.CartId = (int)Session["Cart"];
            repo.AddItemToCart(item);
            TempData["message"] = "Item successfully added to cart!";
        }

        public ActionResult Cart()
        {
            ItemsViewModel vm = new ItemsViewModel();
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            vm.CartId = (int)Session["Cart"];
            vm.ItemsPerCart = repo.GetProductsPerCart(vm.CartId);
            return View(vm);
        }

        [HttpPost]
        public void Delete(int cartId, int productId)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            repo.Delete(cartId, productId);
        }

        [HttpPost]
        public void Edit(ShoppingCartItem item)
        {
            ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
            repo.Edit(item);
        }
    }
}