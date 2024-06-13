//using FoodItem.Models.DbContext;
//using FoodItem.Models.ViewModel;
//using FoodItems.CoustomFillter;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace FoodItems.Controllers
//{
//    //[CustomAuthorize]
//    public class UserController : Controller
//    {
//    practice_547Entities _opetion = new practice_547Entities();
//    public ActionResult Index()
//    {
//        List<RegisterModel> list = _opetion.Database.SqlQuery<RegisterModel>("Exec GetAllData").ToList();
//        return View(list);
//    }
//    [HttpGet]
//    public ActionResult AddProduct()
//    {
//       ViewBag.items = _opetion.ItemMaster.ToList();


//        return View();
//    }
//    [HttpPost]
//   public ActionResult AddProduct(ItemmasterModel itemmasterModel)
//    {
//        int id = itemmasterModel.ItemId;
//        int quentites = itemmasterModel.Quntities;
//        Session["quentity"] = itemmasterModel.Quntities;
//        SqlParameter[] sqlParameters = new SqlParameter[]
//        {
//    new SqlParameter("@itemId", id)
//        };

//        List<ItemmasterModel> itemName = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();

//        for (int i = 0; i < itemName.Count; i++)
//        {
//            itemName[i].Quntities = quentites;
//            itemName[i].TotalAmount = quentites * itemName[i].ItemAmount;
//            itemName[i].CGST = itemName[i].TotalAmount * (decimal)0.05;
//            itemName[i].SGST = itemName[i].CGST;
//            itemName[i].TotalPaybal = itemName[i].TotalAmount + (2 * itemName[i].CGST);
//        }

//        // Update session totals
//        var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
//        var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
//        var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
//        var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;

//        foreach (var item in itemName)
//        {
//            cumulativeTotal += item.TotalAmount;
//            cumulativeCGST += item.CGST;
//            cumulativeSGST += item.SGST;
//            cumulativeFinalPayable += item.TotalPaybal;
//        }

//        // Update session variables
//        Session["CumulativeTotal"] = cumulativeTotal;
//        Session["CumulativeCGST"] = cumulativeCGST;
//        Session["CumulativeSGST"] = cumulativeSGST;
//        Session["CumulativeFinalPayable"] = cumulativeFinalPayable;

//        // Prepare the new row HTML
//        string newRowHtml = "";
//        foreach (var item in itemName)
//        {
//            //SqlParameter[] insertparameter = new SqlParameter[]
//            //    {
//            //    new SqlParameter("@itemName", item.ItemName),
//            //    new SqlParameter("@itemQty",item.Quntities),
//            //    new SqlParameter("@ItemAmount",item.ItemAmount)     
//            //    };
//            //_opetion.Database.SqlQuery<ItemmasterModel>("Exec InsertItem @itemName,@itemQt,@ItemAmount", insertparameter).ToList();
//            newRowHtml += $@"
//        <tr data-itemid='{item.ItemId}'>
//            <td>{item.ItemName}</td>
//            <td>{item.Quntities}</td>
//            <td>{item.ItemAmount}</td>
//            <td>{item.TotalAmount}</td>
//            <td>
//                <button type='button' class='btn btn-primary' onclick='Update(this)'>Edit</button>
//                <button type='button' class='btn btn-danger' onclick='removeEduRow(this)'>Remove</button>
//            </td>
//        </tr>";
//        }

//        // Prepare the cumulative summary HTML
//        string cumulativeSummaryHtml = $@"
//    <tr id='total-summary-row'>
//        <td colspan='3'>Total</td>
//        <td>{cumulativeTotal}</td>
//        <td></td>
//    </tr>
//    <tr id='cgst-summary-row'>
//        <td colspan='3'>CGST</td>
//        <td>{cumulativeCGST}</td>
//        <td></td>
//    </tr>
//    <tr id='sgst-summary-row'>
//        <td colspan='3'>SGST</td>
//        <td>{cumulativeSGST}</td>
//        <td></td>
//    </tr>
//    <tr id='final-summary-row'>
//        <td colspan='3'>Final Payable Amount</td>
//        <td>{cumulativeFinalPayable}</td>
//        <td></td>
//    </tr>";

//        return Json(new { newRowHtml, cumulativeSummaryHtml });
//    }


//    [HttpPost]
//    public ActionResult RemoveProduct(int itemId)
//    {
//        // Retrieve current totals from the session
//        var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
//        var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
//        var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
//        var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;

//        // Fetch the item details from the database to adjust the totals
//        SqlParameter[] sqlParameters = new SqlParameter[]
//        {
//    new SqlParameter("@itemId", itemId)
//        };

//        List< ItemmasterModel> itemToRemove = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();
//        for (int i = 0; i < itemToRemove.Count; i++)
//        {
//            itemToRemove[i].Quntities = Convert.ToInt32(Session["quentity"]);
//            itemToRemove[i].TotalAmount = itemToRemove[i].Quntities * itemToRemove[i].ItemAmount;
//            itemToRemove[i].CGST = itemToRemove[i].TotalAmount * (decimal)0.05;
//            itemToRemove[i].SGST = itemToRemove[i].CGST;
//            itemToRemove[i].TotalPaybal = itemToRemove[i].TotalAmount + (2 * itemToRemove[i].CGST);
//        }
//        foreach (var item in itemToRemove)
//        {

//            // Adjust the totals
//            cumulativeTotal -= item.TotalAmount;
//            cumulativeCGST -= item.CGST;
//            cumulativeSGST -= item.SGST;
//            cumulativeFinalPayable -= item.TotalPaybal;

//            // Update session variables
//            Session["CumulativeTotal"] = cumulativeTotal;
//            Session["CumulativeCGST"] = cumulativeCGST;
//            Session["CumulativeSGST"] = cumulativeSGST;
//            Session["CumulativeFinalPayable"] = cumulativeFinalPayable;
//        }

//        // Prepare the cumulative summary HTML
//        string cumulativeSummaryHtml = $@"
//    <tr id='total-summary-row'>
//        <td colspan='3'>Total</td>
//        <td>{cumulativeTotal}</td>
//        <td></td>
//    </tr>
//    <tr id='cgst-summary-row'>
//        <td colspan='3'>CGST</td>
//        <td>{cumulativeCGST}</td>
//        <td></td>
//    </tr>
//    <tr id='sgst-summary-row'>
//        <td colspan='3'>SGST</td>
//        <td>{cumulativeSGST}</td>
//        <td></td>
//    </tr>
//    <tr id='final-summary-row'>
//        <td colspan='3'>Final Payable Amount</td>
//        <td>{cumulativeFinalPayable}</td>
//        <td></td>
//    </tr>";

//        return Json(new { cumulativeSummaryHtml });
//    }
//    [HttpPost]
//    public ActionResult UpdateProduct(ItemmasterModel itemmasterModel)
//    {
//        int id = itemmasterModel.ItemId;
//        int quantities = itemmasterModel.Quntities;

//        SqlParameter[] sqlParameters = new SqlParameter[]
//        {
//    new SqlParameter("@itemId", id)
//        };

//        List<ItemmasterModel> itemToUpdate = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();

//        foreach (var item in itemToUpdate)
//        {
//            item.Quntities = quantities;
//            item.TotalAmount = quantities * item.ItemAmount;
//            item.CGST = item.TotalAmount * (decimal)0.05;
//            item.SGST = item.CGST;
//            item.TotalPaybal = item.TotalAmount + (2 * item.CGST);
//        }

//        // Prepare the updated row HTML
//        string updatedRowHtml = "";
//        foreach (var item in itemToUpdate)
//        {
//            updatedRowHtml += $@"
//        <tr data-itemid='{item.ItemId}'>
//            <td>{item.ItemName}</td>
//            <td>{item.Quntities}</td>
//            <td>{item.ItemAmount}</td>
//            <td>{item.TotalAmount}</td>
//            <td>
//                <button type='button' class='btn btn-primary edit' onclick='Update(this)'>Edit</button>
//                <button type='button' class='btn btn-danger' onclick='removeEduRow(this)'>Remove</button>
//            </td>
//        </tr>";
//        }

//        return Json(new { updatedRowHtml });
//    }



using FoodItem.Helpers.Session;
using FoodItem.Models.DbContext;
using FoodItem.Models.ViewModel;
using FoodItems.CoustomFillter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodItems.Controllers
{
    [CustomAuthorize]
    public class UserController : Controller
    {
        practice_547Entities _opetion = new practice_547Entities();

        public ActionResult Index()
        {
            List<RegisterModel> list = _opetion.Database.SqlQuery<RegisterModel>("Exec GetAllData").ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.items = _opetion.ItemMaster.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ItemmasterModel itemmasterModel)
        {
            int id = itemmasterModel.ItemId;
            int quentites = itemmasterModel.Quntities;
            Session["quentity"] = itemmasterModel.Quntities;

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@itemId", id)
            };

            List<ItemmasterModel> itemName = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();
            for (int i = 0; i < itemName.Count; i++)
            {
                Session["itemname"] = itemName[i].ItemName;
                itemName[i].Quntities = quentites;
                itemName[i].TotalAmount = quentites * itemName[i].ItemAmount;
                itemName[i].CGST = itemName[i].TotalAmount * (decimal)0.05;
                itemName[i].SGST = itemName[i].CGST;
                itemName[i].TotalPaybal = itemName[i].TotalAmount + (2 * itemName[i].CGST);
            }

            var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
            var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
            var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
            var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;

            foreach (var item in itemName)
            {
                cumulativeTotal += item.TotalAmount;
                cumulativeCGST += item.CGST;
                cumulativeSGST += item.SGST;
                cumulativeFinalPayable += item.TotalPaybal;
            }

            Session["CumulativeTotal"] = cumulativeTotal;
            Session["CumulativeCGST"] = cumulativeCGST;
            Session["CumulativeSGST"] = cumulativeSGST;
            Session["CumulativeFinalPayable"] = cumulativeFinalPayable;

            string newRowHtml = "";
            foreach (var item in itemName)
            {
                newRowHtml += $@"
                <tr data-itemid='{item.ItemId}'>
                    <td>{item.ItemName}</td>
                    <td>{item.Quntities}</td>
                    <td>{item.ItemAmount}</td>
                    <td>{item.TotalAmount}</td>
                    <td>
                        <button type='button' class='btn btn-primary edit' onclick='Update(this)'>Edit</button>
                        <button type='button' class='btn btn-danger' onclick='removeEduRow(this)'>Remove</button>
                    </td>
                </tr>";
            }

            string cumulativeSummaryHtml = $@"
            <tr id='total-summary-row'>
                <td colspan='3'>Total</td>
                <td>{cumulativeTotal}</td>
                <td></td>
            </tr>
            <tr id='cgst-summary-row'>
                <td colspan='3'>CGST</td>
                <td>{cumulativeCGST}</td>
                <td></td>
            </tr>
            <tr id='sgst-summary-row'>
                <td colspan='3'>SGST</td>
                <td>{cumulativeSGST}</td>
                <td></td>
            </tr>
            <tr id='final-summary-row'>
                <td colspan='3'>Final Payable Amount</td>
                <td>{cumulativeFinalPayable}</td>
                <td></td>
            </tr>";

            return Json(new { newRowHtml, cumulativeSummaryHtml });
        }

        [HttpPost]
        public ActionResult RemoveProduct(int itemId)
        {
            var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
            var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
            var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
            var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@itemId", itemId)
            };

            List<ItemmasterModel> itemToRemove = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();
            for (int i = 0; i < itemToRemove.Count; i++)
            {
                itemToRemove[i].Quntities = Convert.ToInt32(Session["quentity"]);
                itemToRemove[i].TotalAmount = itemToRemove[i].Quntities * itemToRemove[i].ItemAmount;
                itemToRemove[i].CGST = itemToRemove[i].TotalAmount * (decimal)0.05;
                itemToRemove[i].SGST = itemToRemove[i].CGST;
                itemToRemove[i].TotalPaybal = itemToRemove[i].TotalAmount + (2 * itemToRemove[i].CGST);
            }
            foreach (var item in itemToRemove)
            {
                cumulativeTotal -= item.TotalAmount;
                cumulativeCGST -= item.CGST;
                cumulativeSGST -= item.SGST;
                cumulativeFinalPayable -= item.TotalPaybal;
            }

            Session["CumulativeTotal"] = cumulativeTotal;
            Session["CumulativeCGST"] = cumulativeCGST;
            Session["CumulativeSGST"] = cumulativeSGST;
            Session["CumulativeFinalPayable"] = cumulativeFinalPayable;

            string cumulativeSummaryHtml = $@"
            <tr id='total-summary-row'>
                <td colspan='3'>Total</td>
                <td>{cumulativeTotal}</td>
                <td></td>
            </tr>
            <tr id='cgst-summary-row'>
                <td colspan='3'>CGST</td>
                <td>{cumulativeCGST}</td>
                <td></td>
            </tr>
            <tr id='sgst-summary-row'>
                <td colspan='3'>SGST</td>
                <td>{cumulativeSGST}</td>
                <td></td>
            </tr>
            <tr id='final-summary-row'>
                <td colspan='3'>Final Payable Amount</td>
                <td>{cumulativeFinalPayable}</td>
                <td></td>
            </tr>";

            return Json(new { cumulativeSummaryHtml });
        }

        [HttpPost]
        public ActionResult UpdateProduct(ItemmasterModel itemmasterModel)
        {
            int id = itemmasterModel.ItemId;
            int quantities = itemmasterModel.Quntities;

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@itemId", id)
            };

            List<ItemmasterModel> itemToUpdate = _opetion.Database.SqlQuery<ItemmasterModel>("Exec getItems @itemId", sqlParameters).ToList();

            foreach (var item in itemToUpdate)
            {
                item.Quntities = quantities;
                item.TotalAmount = quantities * item.ItemAmount;
                item.CGST = item.TotalAmount * (decimal)0.05;
                item.SGST = item.CGST;
                item.TotalPaybal = item.TotalAmount + (2 * item.CGST);
            }

            string updatedRowHtml = "";
            foreach (var item in itemToUpdate)
            {
                updatedRowHtml += $@"
                <tr data-itemid='{item.ItemId}'>
                    <td>{item.ItemName}</td>
                    <td>{item.Quntities}</td>
                    <td>{item.ItemAmount}</td>
                    <td>{item.TotalAmount}</td>
                    <td>
                        <button type='button' class='btn btn-primary edit' onclick='Update(this)'>Edit</button>
                        <button type='button' class='btn btn-danger' onclick='removeEduRow(this)'>Remove</button>
                    </td>
                </tr>";
            }

            return Json(new { updatedRowHtml });
        }

        [HttpPost]
        public ActionResult ApplyPromoCode(string promoCode)
        {
            // Example promo codes and discounts
            var promoCodes = new Dictionary<string, decimal>
            {
                { "DISCOUNT10", 0.10m },
                { "DISCOUNT20", 0.20m }
            };

            if (promoCodes.ContainsKey(promoCode))
            {
                Session["promo"] = promoCode;
                decimal discount = promoCodes[promoCode];
                Session["Discount"] = discount;

                var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
                var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
                var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
                var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;

                decimal discountedAmount = cumulativeFinalPayable * discount;
                decimal finalAmount = cumulativeFinalPayable - discountedAmount;

                Session["DiscountedAmount"] = discountedAmount;
                Session["FinalAmount"] = finalAmount;

                string promoSummaryHtml = $@"
                <tr id='promo-summary-row'>
                    <td colspan='3'>Discount ({promoCode})</td>
                    <td>-{discountedAmount}</td>
                    <td></td>
                </tr>
                <tr id='final-amount-summary-row'>
                    <td colspan='3'>Final Amount After Discount</td>
                    <td>{finalAmount}</td>
                    <td></td>
                </tr>";

                return Json(new { promoSummaryHtml });
            }
            else
            {
                return Json(new { error = "Invalid promo code." });
            }
        }

        [HttpPost]
        public ActionResult SubmitOrder()
        {
            var cumulativeTotal = Session["CumulativeTotal"] as decimal? ?? 0;
            var cumulativeCGST = Session["CumulativeCGST"] as decimal? ?? 0;
            var cumulativeSGST = Session["CumulativeSGST"] as decimal? ?? 0;
            var cumulativeFinalPayable = Session["CumulativeFinalPayable"] as decimal? ?? 0;
            var discount = Session["Discount"] as decimal? ?? 0;
            var discountedAmount = Session["DiscountedAmount"] as decimal? ?? 0;
            var finalAmount = Session["FinalAmount"] as decimal? ?? 0;
            var quntity = Convert.ToInt32(Session["quentity"]);

            SqlParameter[] sqlparm = new SqlParameter[] {
              
            new SqlParameter("@ItemName",Session["itemname"]),
            new SqlParameter("@TotalItems",quntity),
            new SqlParameter("@TotalAmount",cumulativeTotal),
            new SqlParameter("@Cgst",cumulativeCGST),
            new SqlParameter("@Sgst",cumulativeSGST),
            new SqlParameter("@PaybleAmount",cumulativeFinalPayable),
            new SqlParameter("@NetPaybleAmount",finalAmount),
            new SqlParameter("@PromoCode",Session["promo"]),
            new SqlParameter("@UserId", GetSession.GetStudentId),
            };

            _opetion.Database.SqlQuery<OrderModel>("Exec insertOrder @ItemName, @TotalItems,@TotalAmount,@Cgst,@Sgst,@PaybleAmount,@NetPaybleAmount,@PromoCode,@UserId", sqlparm).ToList();

            Session["CumulativeTotal"] = null;
            Session["CumulativeCGST"] = null;
            Session["CumulativeSGST"] = null;
            Session["CumulativeFinalPayable"] = null;
            Session["Discount"] = null;
            Session["DiscountedAmount"] = null;
            Session["FinalAmount"] = null;

            return Json(new { success = true });
        }
    }
}


