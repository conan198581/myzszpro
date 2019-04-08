using RPZSZ.Common;
using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class IdNameController : Controller
    {
        public IIdNameService IdNameService { get; set; }
        // GET: IdName
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var datalist = IdNameService.GetAll();
            return View(datalist);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string typename, string name)
        {
            var idnamedto = new IdNameDTO()
            {
                TypeName = typename,
                Name = name
            };
            IdNameService.Add(idnamedto);
            return Json(new AjaxResult<string> { Status = "ok" });
        }
    }
}