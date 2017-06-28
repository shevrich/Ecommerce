using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data;
using ECommerce.Models;
using System.IO;

namespace ECommerce.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConStr);
            ItemsViewModel vm = new ItemsViewModel();
            vm.categories = repo.GetCategories();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConStr);
            repo.AddCategory(category);
            return Redirect("/admin/index");
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, string name, HttpPostedFileBase file)
        {
            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConStr);
            product.CategoryId = repo.GetCategory(name).Id;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            fileName += extension;
            file.SaveAs(Server.MapPath("~/Images/") + fileName);
            product.ImageFile = fileName;
            repo.AddProduct(product);
            return Redirect("/admin/index");
        }
        
    }
}