using System;
using System.Collections.Generic;

namespace BE.Core
{
    public class Sys_Dashboard
    {
        public string TotalItems { get; set; }
        public string TotalPurchaseAmount { get; set; }
        public string TotalSalesAmount { get; set; }
        public string TotalExpenseAmount { get; set; }
        public string TotalProfitAmount { get; set; }
        public string TotalLossAmount { get; set; }

        public string TotalMonthlyPurchaseAmount { get; set; }
        public string TotalMonthlySaleAmount { get; set; }
        public string TotalMonthlyExpenseAmount { get; set; }
        public string TotalMonthlyProfitAmount { get; set; }
        public string TotalMonthlyLossAmount { get; set; }

        public string TotalDailyPurchaseAmount { get; set; }
        public string TotalDailySaleAmount { get; set; }
        public string TotalDailyExpenseAmount { get; set; }
        public string TotalDailyProfitAmount { get; set; }
        public string TotalDailyLossAmount { get; set; }



        public string PurchaseItemId { get; set; }
        public decimal PurchaseBuyingRate { get; set; }
        public decimal PurchaseQuantity { get; set; }

        public string SalesItemId { get; set; }
        public decimal SalesRate { get; set; }
        public decimal SalesQuantity { get; set; }
        public DateTime EventDate { get; set; }



        public string ChartTotalProfit { get; set; }
        public string ChartTotalLoss { get; set; }
        public string ChartTotalPurchaseAmount { get; set; }
        public string ChartTotalSaleAmount { get; set; }
        public string ChartTotalExpenseAmount { get; set; }

        public List<M_Product> LowStockItemsList { get; set; }


        public decimal TotalLoanAmount { get; set; }
        public decimal TotalLoanPaidAmount { get; set; }
        public decimal TotalLoanOutstandingAmount { get; set; }

        public decimal TotalOutStandingAmount { get; set; }
        public decimal TotalOutStandingPaidAmount { get; set; }
        public decimal TotalOutStandingDueAmount { get; set; }
    }
}
