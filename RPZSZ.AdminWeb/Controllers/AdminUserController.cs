using RPZSZ.AdminWeb.Filters;
using RPZSZ.AdminWeb.Models;
using RPZSZ.Common;
using RPZSZ.CommonMVC;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class AdminUserController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        public IRoleService RoleService { get; set; }
        public ICityService CityService { get; set; }


        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }

        [CheckAuthorize("Admin.Add")]
        [CheckAuthorize("Admin.Update")]
        [CheckAuthorize("Admin.Delete")]
        public ActionResult List()
        {
            var adminUserList = AdminUserService.GetAll();
            return View(adminUserList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            AdminUserAddGetModel viewModel = new AdminUserAddGetModel();

            var roleList = RoleService.GetAll();
            var cityList = CityService.GetAll().ToList();
            cityList.Insert(0, new DTO.CityDTO() { Id = 0, Name = "总部" });
            viewModel.Roles = roleList;
            viewModel.Cities = cityList;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(AdminUerAddPostModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                string msg = MVCHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = msg });
            }
            var item = AdminUserService.GetByPhoneNum(viewModel.PhoneNum);
            if (item != null)
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "该手机号已经存在" });
            }
            long? cityId = null;
            if (viewModel.CityId != 0)
            {
                cityId = viewModel.CityId;
            }
            long adminUserId = AdminUserService.Add(viewModel.Name, viewModel.PhoneNum, viewModel.Email, viewModel.Password, cityId);
            AdminUserService.UpdateRoleByAdminUserId(adminUserId, viewModel.roleIds);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var viewModel = new AdminUserEditGetModel();
            viewModel.AdminUserDTO = AdminUserService.GetById(id);
            var cityList = CityService.GetAll().ToList();
            cityList.Insert(0, new DTO.CityDTO() { Id = 0, Name = "总部" });
            viewModel.Cities = cityList;
            viewModel.RoleDTOs = RoleService.GetAll();
            viewModel.OwnRoleDTOs = RoleService.GetRoleByUserId(id);
            return View(viewModel);

        }

        [HttpPost]
        public ActionResult Edit(AdminUserEditPostModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                string msg = MVCHelper.GetValidMsg(ModelState);
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = msg });
            }
            long? cityId = null;
            if (viewModel.CityId != 0)
            {
                cityId = viewModel.CityId;
            }
            AdminUserService.UpdateAdminUser(viewModel.Id, viewModel.Name, viewModel.PhoneNum, viewModel.Email, viewModel.Password, cityId);
            AdminUserService.UpdateRoleByAdminUserId(viewModel.Id, viewModel.RoleIds);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        public ActionResult Delete(long id)
        {
            AdminUserService.MarkDeleted(id);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        public ActionResult BatchDelete(long[] ids)
        {
            AdminUserService.BatchDelete(ids);
            return Json(new AjaxResult<string> { Status = "ok" });
        }
    }
}