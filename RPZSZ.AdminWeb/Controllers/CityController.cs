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

        public ActionResult Add()
        {
            //CityService.AddNewCity("北京");
            return Content("ok");
        }
    }
}