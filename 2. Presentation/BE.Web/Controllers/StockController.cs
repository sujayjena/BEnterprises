﻿using BE.Core;
using BE.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    [CustomAuthentication]
    public class StockController : Controller
    {
        protected readonly CustomAuthentication _objAuthentication = new CustomAuthentication();
        protected readonly bl_Items _blItems = new bl_Items();

        // GET: Stock
        public ActionResult Index()
        {
            var vItemsTypeList = _blItems.GetItemsTypeList().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            ViewBag.ItemsTypeList = vItemsTypeList;
            return View();
        }

        [HttpPost]
        public ActionResult GetStockList(M_Items ObjItems)
        {
            List<M_Items> ObjList = new List<M_Items>();
            try
            {
                ObjList = _blItems.GetStockList(ObjItems).OrderBy(x => x.ItemsTypeName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(ObjList, JsonRequestBehavior.AllowGet);
        }
    }
}