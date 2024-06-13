using FoodItem.Models.DbContext;
using FoodItem.Models.ViewModel;
using FoodItem.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Repository.Services
{
   public class RegisterServices : IRegisterInterfaces
    {
        practice_547Entities _opetion = new practice_547Entities();
        public List<StateModel> GetStateByCountry(int CountryId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@CountryId",CountryId)
                };
            List<StateModel> getState = _opetion.Database.SqlQuery<StateModel>("exec GetStateId @CountryId", sqlParameters).ToList();
            //Json(getState, JsonRequestBehavior.AllowGet);
            return getState;
        }
        public List<CityModel> GetCityStateId(int stateid)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@StateId",stateid)
            };
            List<CityModel> getCity = _opetion.Database.SqlQuery<CityModel>("Exec GetCityByCountryId @StateId", sqlParameters).ToList();
            return getCity;
        }
    }
}
