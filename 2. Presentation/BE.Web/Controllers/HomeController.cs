using BE.Core;
using BE.Data.Dashboard;
using BE.Data.Items;
using BE.Data.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class HomeController : Controller
    {
        protected readonly bl_Dashboard _blDashboard = new bl_Dashboard();
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_Items _blItems = new bl_Items();

        public ActionResult Index()
        {
            var model = new Sys_Dashboard();

            var vObjItemsList = _blDashboard.GetItems();
            var vObjPurchaseOrderList = _blDashboard.GetPurchaseOrder();
            var vObjSalesOrderList = _blDashboard.GetSalesOrder();
            var vObjGetExpenseList = _blDashboard.GetExpense();
            var vObjSalesAndPurchaseList = _blDashboard.GetSalesAndPurchaseList();
            var vObjLoanList = _blDashboard.GetLoanList();
            var vObjOutStandingList = _blDashboard.GetOutStandingList();

            if (vObjItemsList != null)
            {
                model.TotalItems = Convert.ToString(vObjItemsList.Count);
            }
            if (vObjPurchaseOrderList != null)
            {
                model.TotalPurchaseAmount = Convert.ToString(vObjPurchaseOrderList.Sum(x => x.TotalAmount));
                model.TotalMonthlyPurchaseAmount = Convert.ToString(vObjPurchaseOrderList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year).Sum(x => x.TotalAmount));
                model.TotalDailyPurchaseAmount = Convert.ToString(vObjPurchaseOrderList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year && x.EventDate.Day == DateTime.Now.Day).Sum(x => x.TotalAmount));
            }
            if (vObjSalesOrderList != null)
            {
                model.TotalSalesAmount = Convert.ToString(vObjSalesOrderList.Sum(x => x.TotalAmount));
                model.TotalMonthlySaleAmount = Convert.ToString(vObjSalesOrderList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year).Sum(x => x.TotalAmount));
                model.TotalDailySaleAmount = Convert.ToString(vObjSalesOrderList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year && x.EventDate.Day == DateTime.Now.Day).Sum(x => x.TotalAmount));
            }
            if (vObjGetExpenseList != null)
            {
                model.TotalExpenseAmount = Convert.ToString(vObjGetExpenseList.Sum(x => x.Amount));
                model.TotalMonthlyExpenseAmount = Convert.ToString(vObjGetExpenseList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year).Sum(x => x.Amount));
                model.TotalDailyExpenseAmount = Convert.ToString(vObjGetExpenseList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year && x.EventDate.Day == DateTime.Now.Day).Sum(x => x.Amount));
            }
            if (vObjLoanList != null)
            {
                model.TotalLoanAmount = vObjLoanList.Sum(x => x.Amount);
                model.TotalLoanPaidAmount = vObjLoanList.Sum(x => x.PaidAmount);
                model.TotalLoanOutstandingAmount = vObjLoanList.Sum(x => x.DueAmount);
            }
            if (vObjOutStandingList != null)
            {
                model.TotalOutStandingAmount = vObjOutStandingList.Sum(x => x.Amount);
                model.TotalOutStandingPaidAmount = vObjOutStandingList.Sum(x => x.PaidAmount);
                model.TotalOutStandingDueAmount = vObjOutStandingList.Sum(x => x.DueAmount);
            }

            // Total Profit Loss
            if (vObjSalesAndPurchaseList.Count > 0)
            {
                decimal vTotalProfit = 0;
                decimal vTotalLoss = 0;
                foreach (var item in vObjSalesAndPurchaseList)
                {
                    var vpurchaseItemId = item.PurchaseItemId;
                    var vpurchaseRate = item.PurchaseBuyingRate;
                    var vpurchaseQuantity = item.PurchaseQuantity;
                    var vsalesItemId = item.SalesItemId;
                    var vsalesRate = item.SalesRate;
                    var vsalesQuantity = item.SalesQuantity;

                    if (vsalesRate > vpurchaseRate)
                    {
                        //var vPerquantityRate = (vsalesRate / vsalesQuantity);
                        var vSellingRateMinusPurchaseRateDiff = (vsalesRate - vpurchaseRate);
                        vTotalProfit += vSellingRateMinusPurchaseRateDiff;

                        //var vAmountOfPercent = (vSellingRateMinusPurchaseRateDiff * vPercent / 100);
                        //var vSellingAmountAfterMinusPercentage = (vSellingRateMinusPurchaseRateDiff - vAmountOfPercent);
                        //if (vSellingAmountAfterMinusPercentage > vAmountOfPercent)
                        //{
                        //    vTotalProfit += vSellingRateMinusPurchaseRateDiff;
                        //}
                        //vPerquantityRate = 0;
                        vSellingRateMinusPurchaseRateDiff = 0;
                        // vAmountOfPercent = 0;
                        //vSellingAmountAfterMinusPercentage = 0;
                    }
                    else
                    {
                        //var vPerquantityRate = (vsalesRate / vsalesQuantity);
                        var vSellingRateMinusPurchaseRateDiff = (vpurchaseRate - vsalesRate);
                        vTotalLoss += vSellingRateMinusPurchaseRateDiff;
                        // vPerquantityRate = 0;
                        vSellingRateMinusPurchaseRateDiff = 0;
                    }
                    vpurchaseItemId = string.Empty;
                    vpurchaseRate = 0;
                    vpurchaseQuantity = 0;
                    vsalesItemId = string.Empty;
                    vsalesRate = 0;
                    vsalesQuantity = 0;
                }
                model.TotalProfitAmount = Convert.ToString(vTotalProfit);
                model.TotalLossAmount = Convert.ToString(vTotalLoss);
            }

            // Monthly Profit Loss
            if (vObjSalesAndPurchaseList.Count > 0)
            {
                var vCurrentMonthList = vObjSalesAndPurchaseList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year).ToList();
                if (vCurrentMonthList.Count > 0)
                {
                    decimal vTotalProfit = 0;
                    decimal vTotalLoss = 0;
                    foreach (var item in vCurrentMonthList)
                    {
                        var vpurchaseItemId = item.PurchaseItemId;
                        var vpurchaseRate = item.PurchaseBuyingRate;
                        var vpurchaseQuantity = item.PurchaseQuantity;
                        var vsalesItemId = item.SalesItemId;
                        var vsalesRate = item.SalesRate;
                        var vsalesQuantity = item.SalesQuantity;

                        if (vsalesRate > vpurchaseRate)
                        {
                            var vSellingRateMinusPurchaseRateDiff = (vsalesRate - vpurchaseRate);
                            vTotalProfit += vSellingRateMinusPurchaseRateDiff;
                            vSellingRateMinusPurchaseRateDiff = 0;
                        }
                        else
                        {
                            var vSellingRateMinusPurchaseRateDiff = (vpurchaseRate - vsalesRate);
                            vTotalLoss += vSellingRateMinusPurchaseRateDiff;
                            vSellingRateMinusPurchaseRateDiff = 0;
                        }
                        vpurchaseItemId = string.Empty;
                        vpurchaseRate = 0;
                        vpurchaseQuantity = 0;
                        vsalesItemId = string.Empty;
                        vsalesRate = 0;
                        vsalesQuantity = 0;
                    }
                    model.TotalMonthlyProfitAmount = Convert.ToString(vTotalProfit);
                    model.TotalMonthlyLossAmount = Convert.ToString(vTotalLoss);
                }
                else
                {
                    model.TotalMonthlyProfitAmount = "0";
                    model.TotalMonthlyLossAmount = "0";
                }
            }

            // Daily Profit Loss
            if (vObjSalesAndPurchaseList.Count > 0)
            {
                var vDailyList = vObjSalesAndPurchaseList.Where(x => x.EventDate.Month == DateTime.Now.Month && x.EventDate.Year == DateTime.Now.Year && x.EventDate.Day == DateTime.Now.Day).ToList();
                if (vDailyList.Count > 0)
                {
                    decimal vTotalProfit = 0;
                    decimal vTotalLoss = 0;
                    foreach (var item in vDailyList)
                    {
                        var vpurchaseItemId = item.PurchaseItemId;
                        var vpurchaseRate = item.PurchaseBuyingRate;
                        var vpurchaseQuantity = item.PurchaseQuantity;
                        var vsalesItemId = item.SalesItemId;
                        var vsalesRate = item.SalesRate;
                        var vsalesQuantity = item.SalesQuantity;

                        if (vsalesRate > vpurchaseRate)
                        {
                            var vSellingRateMinusPurchaseRateDiff = (vsalesRate - vpurchaseRate);
                            vTotalProfit += vSellingRateMinusPurchaseRateDiff;
                            vSellingRateMinusPurchaseRateDiff = 0;
                        }
                        else
                        {
                            var vSellingRateMinusPurchaseRateDiff = (vpurchaseRate - vsalesRate);
                            vTotalLoss += vSellingRateMinusPurchaseRateDiff;
                            vSellingRateMinusPurchaseRateDiff = 0;
                        }
                        vpurchaseItemId = string.Empty;
                        vpurchaseRate = 0;
                        vpurchaseQuantity = 0;
                        vsalesItemId = string.Empty;
                        vsalesRate = 0;
                        vsalesQuantity = 0;
                    }
                    model.TotalDailyProfitAmount = Convert.ToString(vTotalProfit);
                    model.TotalDailyLossAmount = Convert.ToString(vTotalLoss);
                }
                else
                {
                    model.TotalDailyProfitAmount = "0";
                    model.TotalDailyLossAmount = "0";
                }
            }

            // Chart 
            // Every Month Profit 
            if (vObjSalesAndPurchaseList.Count > 0)
            {
                string sTotalProfitCommaSprateVal = string.Empty;
                string sTotalLossCommaSprateVal = string.Empty;
                string sTotalPurchaseCommaSprateVal = string.Empty;
                string sTotalSalesCommaSprateVal = string.Empty;
                string sTotalExpenseCommaSprateVal = string.Empty;
                for (int i = 1; i <= 12; i++)
                {
                    decimal vTotalProfit = 0;
                    decimal vTotalLoss = 0;
                    var vCurrentMonthList = vObjSalesAndPurchaseList.Where(x => x.EventDate.Month == i && x.EventDate.Year == DateTime.Now.Year).ToList();
                    if (vCurrentMonthList.Count > 0)
                    {
                        foreach (var item in vCurrentMonthList)
                        {
                            var vpurchaseRate = item.PurchaseBuyingRate;
                            var vsalesRate = item.SalesRate;

                            if (vsalesRate > vpurchaseRate)
                            {
                                var vSellingRateMinusPurchaseRateDiff = (vsalesRate - vpurchaseRate);
                                vTotalProfit += vSellingRateMinusPurchaseRateDiff;
                                vSellingRateMinusPurchaseRateDiff = 0;
                            }
                            else
                            {
                                var vSellingRateMinusPurchaseRateDiff = (vpurchaseRate - vsalesRate);
                                vTotalLoss += vSellingRateMinusPurchaseRateDiff;
                                vSellingRateMinusPurchaseRateDiff = 0;
                            }
                            vpurchaseRate = 0;
                            vsalesRate = 0;
                        }
                        sTotalProfitCommaSprateVal += Convert.ToString(vTotalProfit) + ",";
                        sTotalLossCommaSprateVal += Convert.ToString(vTotalLoss) + ",";
                    }
                    else
                    {
                        sTotalProfitCommaSprateVal += "0" + ",";
                        sTotalLossCommaSprateVal += "0" + ",";
                    }

                    var vPurchaseOrderList = vObjPurchaseOrderList.Where(x => x.EventDate.Month == i && x.EventDate.Year == DateTime.Now.Year).ToList();
                    if (vPurchaseOrderList.Count > 0)
                    {
                        sTotalPurchaseCommaSprateVal += Convert.ToString(vObjPurchaseOrderList.Sum(x => x.TotalAmount)) + ",";
                    }
                    else
                    {
                        sTotalPurchaseCommaSprateVal += "0" + ",";
                    }

                    var vSalesOrderList = vObjSalesOrderList.Where(x => x.EventDate.Month == i && x.EventDate.Year == DateTime.Now.Year).ToList();
                    if (vSalesOrderList.Count > 0)
                    {
                        sTotalSalesCommaSprateVal += Convert.ToString(vSalesOrderList.Sum(x => x.TotalAmount)) + ",";
                    }
                    else
                    {
                        sTotalSalesCommaSprateVal += "0" + ",";
                    }

                    var vExpenseList = vObjGetExpenseList.Where(x => x.EventDate.Month == i && x.EventDate.Year == DateTime.Now.Year).ToList();
                    if (vExpenseList.Count > 0)
                    {
                        sTotalExpenseCommaSprateVal += Convert.ToString(vExpenseList.Sum(x => x.Amount)) + ",";
                    }
                    else
                    {
                        sTotalExpenseCommaSprateVal += "0" + ",";
                    }
                }

                sTotalProfitCommaSprateVal = sTotalProfitCommaSprateVal.Substring(0, sTotalProfitCommaSprateVal.Length - 1);
                sTotalLossCommaSprateVal = sTotalLossCommaSprateVal.Substring(0, sTotalLossCommaSprateVal.Length - 1);
                sTotalPurchaseCommaSprateVal = sTotalPurchaseCommaSprateVal.Substring(0, sTotalPurchaseCommaSprateVal.Length - 1);
                sTotalSalesCommaSprateVal = sTotalSalesCommaSprateVal.Substring(0, sTotalSalesCommaSprateVal.Length - 1);
                sTotalExpenseCommaSprateVal = sTotalExpenseCommaSprateVal.Substring(0, sTotalExpenseCommaSprateVal.Length - 1);

                model.ChartTotalProfit = sTotalProfitCommaSprateVal;
                model.ChartTotalLoss = sTotalLossCommaSprateVal;
                model.ChartTotalPurchaseAmount = sTotalPurchaseCommaSprateVal;
                model.ChartTotalSaleAmount = sTotalSalesCommaSprateVal;
                model.ChartTotalExpenseAmount = sTotalExpenseCommaSprateVal;
            }

            model.LowStockItemsList = GetStockItem();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CloseWindows()
        {
            bl_User _objUser = new bl_User();

            DateTime dCurrentLoginTime = DateTime.Now;
            string sCurrentLoginIP = string.Empty;

            // ********************************** // ***************************************
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            sCurrentLoginIP = Dns.GetHostByName(hostName).AddressList[0].ToString(); // Get the 

            #region Update Login Detail

            if (!string.IsNullOrWhiteSpace(_objAuthentication.UserName) && !string.IsNullOrWhiteSpace(_objAuthentication.Id))
            {
                Guid guid = new Guid(_objAuthentication.Id);

                var vObj = _objUser.GetById(guid);
                if (vObj != null)
                {
                    vObj.LastLoginTime = DateTime.Now;
                    vObj.LastLoginIP = sCurrentLoginIP;
                    _objUser.Update(vObj);
                }

                HttpCookie authCookie = HttpContext.Request.Cookies["UserInfo"];
                if (authCookie != null)
                {
                    authCookie.Values["LastLoginTime"] = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy") + ",  " + DateTime.Now.ToString("hh:mm:ss tt"));
                    authCookie.Values["LastLoginIP"] = sCurrentLoginIP;
                    Response.SetCookie(authCookie);
                }
            }

            #endregion

            return View();
        }

        public List<M_Items> GetStockItem()
        {
            M_Items ObjItems = new M_Items();
            var vObjList = _blItems.GetStockList(ObjItems).Where(x => x.StockQuantity < 5);
            return vObjList.ToList();
        }
    }
}