using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data;

namespace ECommerce
{
    public class CartAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session["Cart"] != null)
            {
                ItemsRepository repo = new ItemsRepository(Properties.Settings.Default.ConStr);
                int id = (int)filterContext.HttpContext.Session["Cart"];
                filterContext.Controller.ViewBag.Cart = repo.GetItemsCount(id);
                base.OnActionExecuting(filterContext);
            }
         
        }
    }
}