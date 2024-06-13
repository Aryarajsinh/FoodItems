using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Models.ViewModel
{
    public class OrderModel
    {
       public string TotalItems { get; set; }
       public string TotalAmount {get; set;}
       public string Cgst {get; set;}
       public string Sgst {get; set;}
       public string PaybleAmount {get; set;}
       public string NetPaybleAmount {get; set;}
       public string PromoCode {get; set;}
       public string OrderDate {get; set;}
       public string UserId {get; set;}   
    }
}
