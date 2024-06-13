using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Models.ViewModel
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<int> CountryId { get; set; }
    }
}
