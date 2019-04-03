using RPZSZ.Common;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class CityController : Controller
    {
        public ICityService CityService { get; set; }
        // GET: City
        public ActionResult Index()
        {
            
            return View();
        }


        public ActionResult List()
        {
            var cityList = CityService.GetAll();
            return View(cityList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            CityService.AddNewCity(name);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var citydto = CityService.GetById(id);
            return View(citydto);
        }

        [HttpPost]
        public ActionResult Edit(long id, string name)
        {
            if (CityService.CheckCityByName(name))
            {
                return Json(new AjaxResult<string> { Status = "error", ErrorMsg = "该城市已经存在" });
            }
            CityService.Update(id, name);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            CityService.Delete(id);
            return Json(new AjaxResult<string> { Status = "ok" });
        }
    }
}