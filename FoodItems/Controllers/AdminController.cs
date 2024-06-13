using FoodItem.Models.DbContext;
using FoodItems.CoustomFillter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodItems.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {

        practice_547Entities _opetion = new practice_547Entities();
        public ActionResult Index()
        {            
            return View(_opetion.Registration.ToList());
        }
    }
}