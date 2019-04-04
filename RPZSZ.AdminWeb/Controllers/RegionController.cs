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
    public class RegionController : Controller
    {
        public IRegionService RegionService { get; set; }
        public ICityService CityService { get; set; }
        // GET: Region
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var regionList = RegionService.GetAll();
            return View(regionList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var cityList = CityService.GetAll();
            return View(cityList);
        }
        [HttpPost]
        public ActionResult Add(RegionAddPostModel viewModel)
        {
            var regionid = RegionService.Add(viewModel.cityId, viewModel.Name);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var regionEditGetModel = new RegionEditGetModel();
            regionEditGetModel.RegionDTO = RegionService.GetById(id);
            regionEditGetModel.CityDTOs = CityService.GetAll();
            return View(regionEditGetModel);
        }

        public ActionResult Edit(RegionEditPostModel postViewModel)
        {
            RegionService.Update(postViewModel.Id, postViewModel.Name, postViewModel.CityId);
            return Json(new AjaxResult<string> { Status = "ok" });
        }


        public ActionResult GetRegionByCityId(long cityId)
        {
            var regionList = RegionService.GetRegionsByCityId(cityId);
            return Json(new AjaxResult<RegionDTO[]> { Status = "ok", Data = regionList });
        }
    }
}