using FoodItem.Helpers.Session;
using FoodItem.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodItems.CoustomFillter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                practice_547Entities _option = new practice_547Entities();
                Registration list = new Registration();

                //Students _RegistersUserUsername = StudentHelper.convertStudentToStudentModelUSingObj(db);
                list = _option.Registration.FirstOrDefault(m => m.Username == GetSession.LoggedInUser);


                if (list != null)
                {
                    return GetSession.IsUserLoggedIn;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            try
            {
                if (!GetSession.IsUserLoggedIn)
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                }
                else
                {     // Not Direct Acess Home/Index Using This Methods 

                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}