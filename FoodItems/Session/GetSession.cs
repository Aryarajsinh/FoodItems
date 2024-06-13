using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FoodItem.Helpers.Session
{
   public class GetSession
    {
        private const string Student_SESSION_KEY = "UserName";

        public static string LoggedInUser
        {
            get { return HttpContext.Current.Session[Student_SESSION_KEY] as string; }
            set { HttpContext.Current.Session[Student_SESSION_KEY] = value; }
        }

        public static int GetStudentId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["UserId"];
            }

            set
            {

                HttpContext.Current.Session["UserId"] = value;

            }

        }

        public static bool IsUserLoggedIn
        {
            get
            {
                if (HttpContext.Current.Session[Student_SESSION_KEY] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // Method to log out the user
        public static void LogOutUser()
        {
            HttpContext.Current.Session.Remove(Student_SESSION_KEY);
            HttpContext.Current.Session.Abandon();
        }


    }
}
