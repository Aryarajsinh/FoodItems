using FoodItem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Repository.Interfaces
{
   public interface IRegisterInterfaces
    {
        List<StateModel> GetStateByCountry(int CountryId);
    }
}
