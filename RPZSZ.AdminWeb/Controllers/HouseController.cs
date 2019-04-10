using RPZSZ.AdminWeb.Tools;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Controllers
{
    public class HouseController : Controller
    {
        public IHouseService HouseService { get; set; }
        public IAdminUserService AdminUserService { get; set; }

        public IIdNameService IdNameService { get; set; }
        
        // GET: House
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string roomType,string name,int pageIndex = 1)
        {
            long userId = (long)AdminHelper.GetSessionAdminUserId(HttpContext);
            long? cityId = AdminUserService.GetById(userId).CityId;
            if (cityId == null)
            {
                return Content("总部人员，不能参与房源业务");
            }
            long roomTypeId = IdNameService.GetByName(roomType, name).Id;
            var houseList = HouseService.GetPagedData(cityId.Value, roomTypeId, 10, (pageIndex - 1) * 10);
            return View(houseList);
        }

        public ActionResult Add()
        {


            return View();
        }
    }
}