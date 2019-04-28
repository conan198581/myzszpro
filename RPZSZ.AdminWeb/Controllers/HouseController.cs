using CodeCarvings.Piczard;
using CodeCarvings.Piczard.Filters.Watermarks;
using RPZSZ.AdminWeb.Models;
using RPZSZ.AdminWeb.Tools;
using RPZSZ.Common;
using RPZSZ.DTO;
using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IAttachmentService AttachmentService { get; set; }

        public IRegionService RegionService { get; set; }

        public IHousePicService HousePicService { get; set; }
        
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
            var houselistviewmodel = new HouseListModel();
            houselistviewmodel.RoomTypeId = roomTypeId;
            houselistviewmodel.HouseList = houseList;
            return View(houselistviewmodel);
        }

        [HttpGet]
        public ActionResult Add(long id)
        {
            var houseAddGetModel = new HouseAddGetModel();
            long userId = (long)AdminHelper.GetSessionAdminUserId(HttpContext);
            long? cityId = AdminUserService.GetById(userId).CityId;
            if (cityId == null)
            {
                return Content("总部人员，不能参与房源业务");
            }
            houseAddGetModel.Regions = RegionService.GetRegionsByCityId(cityId.Value);
            houseAddGetModel.Types = IdNameService.GetByTypeName("房型");
            houseAddGetModel.Status = IdNameService.GetByTypeName("状态");
            houseAddGetModel.DecorateStatus = IdNameService.GetByTypeName("装修状态");
            houseAddGetModel.Attachments = AttachmentService.GetAll();
            houseAddGetModel.RoomTypeId = id;
            return View(houseAddGetModel);
        }

        [HttpPost]
        public ActionResult Add(HouseAddPostModel viewModel)
        {
            HouseDTO houseDTO = new HouseDTO();
            houseDTO.CommunityId = viewModel.CommunityId;
            houseDTO.RoomTypeId = viewModel.RoomTypeId;
            houseDTO.Address = viewModel.Address;
            houseDTO.MonthRent = viewModel.MonthRent;
            houseDTO.StatusId = viewModel.StatusId;
            houseDTO.Area = viewModel.Area;
            houseDTO.DecorateStatusId = viewModel.DecorateStatusId;
            houseDTO.TotalFloorCount = viewModel.TotalFloorCount;
            houseDTO.FloorIndex = viewModel.FloorIndex;
            houseDTO.TypeId = viewModel.TypeId;
            houseDTO.Direction = viewModel.Direction;
            houseDTO.LookableDateTime = viewModel.lookableDateTime;
            houseDTO.CheckInDateTime = viewModel.CheckInDateTime;
            houseDTO.OwnerName = viewModel.OwnerName;
            houseDTO.OwnerPhoneNum = viewModel.ownerPhoneNum;
            houseDTO.Description = viewModel.description;
            long id = HouseService.Add(houseDTO);
            AttachmentService.UpdateAttachementsByHoueId(id, viewModel.attachmentIds);
            return Json(new AjaxResult<string> { Status = "ok" });
        }

        public ActionResult PicUpload(long houseId)
        {
            return View(houseId);
        }


        /*
         * Install-Package CodeCarvings.Piczard 缩略图
         */
        public ActionResult UploadPic(long houseId, HttpPostedFileBase file)
        {
            string filemd5 = CommonHelper.CalcMD5(file.InputStream);
            string fileExt = Path.GetExtension(file.FileName);
            string virtualPath = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") +"/"+ filemd5 + fileExt;
            string thumbPath = "/upload/" + DateTime.Now.ToString("yyyy/MM/dd") + "/" + "thumb_"+filemd5 + fileExt;
            string phyPath = HttpContext.Server.MapPath(virtualPath);
            string thumbPhyPath = HttpContext.Server.MapPath(thumbPath);
            new FileInfo(phyPath).Directory.Create();//如果目录存在 不创建 不存在 则创建
            //file.SaveAs(phyPath);
            //引用缩略图的的包 生成缩略图
            ImageProcessingJob imageProcessingJob = new ImageProcessingJob();
            imageProcessingJob.Filters.Add(new FixedResizeConstraint(200, 200));
            imageProcessingJob.SaveProcessedImageToFileSystem(file.InputStream, thumbPhyPath);

            //生成水印图
            ImageWatermark imgWatermark = new ImageWatermark("~/images/water.jpg");
            imgWatermark.ContentAlignment = System.Drawing.ContentAlignment.BottomRight;//水印位置
            imgWatermark.Alpha = 50;//透明度，需要水印图片是背景透明的 png 图片
            ImageProcessingJob jobNormal = new ImageProcessingJob();
            jobNormal.Filters.Add(imgWatermark);//添加水印
            jobNormal.Filters.Add(new FixedResizeConstraint(600, 600));//限制图片的大小，避免生成大图。如果想原图大小处理，就不用加这个 Filter
            jobNormal.SaveProcessedImageToFileSystem(file.InputStream, phyPath);
            HouseService.AddHousePic(houseId, new HousePicDTO { HouseId = houseId, Url = virtualPath, ThumbUrl = thumbPath });
            return Json(new AjaxResult<string> { Status="ok"});
        }


        public ActionResult ShowPicList(long houseId)
        {
            var housePicList = HouseService.ShowHousePic(houseId);
            return View(housePicList);
        }

        public ActionResult DelPic(long[] housePicIds)
        {
            foreach (var item in housePicIds)
            {
                HousePicService.Delete(item);
            }
            return Json(new AjaxResult<string> { Status = "ok" });
        }

    }
}