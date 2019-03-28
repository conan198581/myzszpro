using RPZSZ.Common;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        public IRoleService RoleService { get; set; }
        public IPermissionService PermissionService { get; set; }
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var itemlist = RoleService.GetAll();
            return View(itemlist);
        }


        [HttpGet]
        public ActionResult Add()
        {
            var list = PermissionService.GetAll();
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(string name, long[] permissionIds)
        {
            long roleId = RoleService.Add(name, permissionIds);
            if (roleId > 0)
            {
                return Json(new AjaxResult<string> { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "新增角色失败" });
            }
            
        }
    }
}