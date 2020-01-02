using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE.Data.Entity;

namespace BE.Web.Controllers
{
    public class ToolController : Controller
    {
        bl_Entity _bl_Entity = new bl_Entity();

        // GET: Tool
        public ActionResult Index()
        {
            _bl_Entity.GenerateModel();
            _bl_Entity.GenerateViewModel();
            _bl_Entity.GenerateUnitOfWork();
            _bl_Entity.GenerateDbContext();

            return View();
        }
    }
}