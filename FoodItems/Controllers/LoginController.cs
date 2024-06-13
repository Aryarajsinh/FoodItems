using FoodItem.Helpers.Session;
using FoodItem.Models.DbContext;
using FoodItem.Models.ViewModel;
using FoodItem.Repository.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodItems.Controllers
{
    public class LoginController : Controller
    {
        practice_547Entities _opetion = new practice_547Entities();
        RegisterServices _registerService = new RegisterServices();
        public ActionResult Login()
        {
            GetSession.LogOutUser();
            return View();
        } 
        [HttpPost]
        public ActionResult Login(LoginModel _login)
        {
            var list = _opetion.Registration.Where(e => e.Email == _login.Email).FirstOrDefault();

            if (_login.Password == list.Password && list.role==1 && ModelState.IsValid  && !GetSession.IsUserLoggedIn)
            {
                GetSession.GetStudentId = list.UserId;
                GetSession.LoggedInUser = list.Username;
                Session["Username"] = GetSession.LoggedInUser;
                Session["userid"] = GetSession.GetStudentId;
                TempData["logintoastr"] = "Login Successssully";
                return RedirectToAction("index", "Admin");
            } 
            else if (_login.Password == list.Password && list.role==2 && ModelState.IsValid && !GetSession.IsUserLoggedIn)
            {
                GetSession.GetStudentId = list.UserId;
                GetSession.LoggedInUser = list.Username;
                TempData["logintoastr"] = "Login Successssully";
                return RedirectToAction("index", "User");
            }
            else
            {
                ModelState.AddModelError("Password", "Please Enter Valid Password");
                ViewBag.loginfailtoastr = "Please Enter Valid Credential";
                return View();
            }
            return View();
        }

        public ActionResult Register()
        {
           ViewBag.Destination = _opetion.Destination.ToList();
           ViewBag.Country =  _opetion.country.ToList();
            return View();
        }
      
        public string ConvertImageTopath(HttpPostedFileBase file)
        {
            string uniqefilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/") + uniqefilename);
            return uniqefilename;
        }
        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string a = ConvertImageTopath(file);
                register.profilePicture = a;
            }
            var isEmailexist = _opetion.Registration.Any(m => m.Email == register.Email);
            if(ModelState.IsValid && !isEmailexist)
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter ("@Username",(object)register.Username ?? DBNull.Value),
                    new SqlParameter ("@FirstName",register.FirstName),
                    new SqlParameter ("@LastName",register.LastName),
                    new SqlParameter ("@Password",register.Password),
                    new SqlParameter ("@Email",register.Email),
                    new SqlParameter ("@hobbies ",string.Join(",", register.hobbies)),
                    new SqlParameter ("@gender",register.gender),
                    new SqlParameter ("@profilePicture ",register.profilePicture),
                    new SqlParameter ("@role ",register.role),
                    new SqlParameter ("@Country",register.Country),
                    new SqlParameter ("@State",register.State),
                    new SqlParameter ("@City",register.City),
                    new SqlParameter ("@DateOfBirth",register.Dob),
                    new SqlParameter ("@Destination",string.Join(",", register.DestinationId)),
                    new SqlParameter ("@PhoneNumber",register.PhoneNumber)
                };
                _opetion.Database.SqlQuery<RegisterModel>("Exec InsertIntoRegistration @Username,@FirstName,@LastName,@Password,@Email,@hobbies,@gender,@profilePicture,@role, @Country, @State, @City, @DateOfBirth, @Destination, @PhoneNumber", sqlParameters).ToList();
                return RedirectToAction("Login");
            }
            if (isEmailexist)
            {

            ModelState.AddModelError("Email", "Email is All Ready Exist");
            }
            ViewBag.Destination = _opetion.Destination.ToList();
            ViewBag.Country = _opetion.country.ToList();
            return View();
        }

        public JsonResult GetStateByCountry(int CountryId)
        {
            List<StateModel> getState = _registerService.GetStateByCountry(CountryId);
            return Json(getState, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityStateId(int Stateid)
        {
            List<CityModel> getCity = _registerService.GetCityStateId(Stateid);
            return Json(getCity, JsonRequestBehavior.AllowGet);
        }
    }
}