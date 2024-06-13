using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Models.ViewModel
{
    public class ItemmasterModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quntities { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal TotalAmount { get; set; }
        
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal TotalPaybal { get; set; }
       
    }
}
