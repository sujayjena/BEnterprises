using BE.Core.ViewModel;
using BE.Data.Entity;
using System;
using System.Web.Mvc;

namespace BE.Web.Controllers
{
    public class ToolController : Controller
    {
        bl_Entity _bl_Entity = new bl_Entity();

        // GET: Tool
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GenerateModel(Sys_GenerateViewModel sys_GenerateViewModel)
        {
            try
            {
                _bl_Entity.GenerateModel(sys_GenerateViewModel);
                return Json(new { Result = true, Message = "Entity model generated" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false,Message= ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GenerateViewModel(Sys_GenerateViewModel sys_GenerateViewModel)
        {
            try
            {
                _bl_Entity.GenerateViewModel(sys_GenerateViewModel);
                return Json(new { Result = true, Message = "Entity view model generated" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GenerateUnitOfWork()
        {
            try
            {
                _bl_Entity.GenerateUnitOfWork();
                return Json(new { Result = true, Message = "Entity unit of work generated" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GenerateDbContext()
        {
            try
            {
                _bl_Entity.GenerateDbContext();
                return Json(new { Result = true, Message = "Entity db context generated" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}