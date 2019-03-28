using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using RPZSZ.DTO;
using RPZSZ.Common;

namespace RPZSZ.AdminWeb.Controllers
{
    public class PermissionController : Controller
    {
        public IPermissionService PermissionService { get; set; }
        // GET: Permission
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var datalist = PermissionService.GetAll();
            return View(datalist);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name, string description)
        {
            var result = PermissionService.Add(name, description);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var item = PermissionService.GetById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(long id, string name, string description)
        {
            int i = PermissionService.Edit(id, name, description);
            if (i > 0)
            {
                return Json(new AjaxResult<string> { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "更新失败" });
            }
        }

        public ActionResult BatchDelete(long[] ids)
        {
            PermissionService.BatchDel(ids);
            return Json(new AjaxResult<string> { Status = "ok" });
        }
    }
}