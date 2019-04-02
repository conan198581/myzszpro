using CaptchaGen;
using RPZSZ.AdminWeb.Models;
using RPZSZ.Common;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class MainController : Controller
    {
        public IAdminUserService AdminUserService { get; set; }
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "modelstate报错信息" });
            }
            if (loginViewModel.VerifyCode != (string)TempData["verifyCode"])
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "验证码错误" });
            }
            bool exists = AdminUserService.CheckLogin(loginViewModel.PhoneNum, loginViewModel.Password);
            if (exists)
            {
                return Json(new AjaxResult<string> { Status = "ok" });
            }
            else
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "用户名或者密码错误" });
            }
        }

        public ActionResult CreateVerifyCode()
        {
            string verifyCode = CommonHelper.CreateVerifyCode(4);
            TempData["verifyCode"] = verifyCode;
            MemoryStream ms = ImageFactory.GenerateImage(verifyCode, 60, 100, 20, 6);
            return File(ms, "image/jpeg");
        }
    }
}