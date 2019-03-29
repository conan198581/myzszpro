using RPZSZ.AdminWeb.Models;
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


        public ActionResult Delete(long id)
        {
            RoleService.Delete(id);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            RoleEditViewModel viewModel = new RoleEditViewModel();
            viewModel.Id = id;
            viewModel.OwnPermissionDTOs = PermissionService.GetByRoleId(id);
            viewModel.PermissionDTOs = PermissionService.GetAll();
            viewModel.Name = RoleService.GetById(id).Name;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(RoleEditPostModel roleEditPostModel)
        {
            RoleService.UpdateRoleAndPermission(roleEditPostModel.Id, roleEditPostModel.Name, roleEditPostModel.PermissionIds);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] ids)
        {
            RoleService.BatchDelete(ids);
            return Json(new AjaxResult<string> { Status = "ok" });
        }
    }
}