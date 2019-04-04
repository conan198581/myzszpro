using RPZSZ.AdminWeb.Models;
using RPZSZ.Common;
using RPZSZ.DTO;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class CommunityController : Controller
    {
        public ICommunityService CommunityService { get;set; }
        public ICityService CityService { get; set; }
        public IRegionService RegionService { get; set; }
        // GET: Community
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var communityList = CommunityService.GetAll();
            return View(communityList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            CommunityAddGetModel vmodel = new CommunityAddGetModel();
            var citylist = CityService.GetAll();
            vmodel.CityDTOs = citylist;
            return View(vmodel);
        }

        [HttpPost]
        public ActionResult Add(CommunityDTO dto)
        {
            //CommunityService.Add()
            CommunityService.Add(dto);
            return Json(new AjaxResult<string> { Status="ok"});
        }

        public ActionResult Edit(long id)
        {
            return View();
        }
    }
}