using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service
{
    public class HouseService : IHouseService
    {
        public HouseDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var datalist = baseService.GetAll().Include(x => x.Community).Include(x => x.Community.Region).Include(x => x.Community.Region.City).Include(x => x.Attachments).Include(x => x.HousePics).Include(x => x.RoomType).Include(x => x.Status).Include(x => x.DecorateStatus).Include(x => x.Type);
                if (datalist == null || datalist.Count() < 1)
                {
                    throw new Exception("没有相关数据");
                }
                return datalist.ToList().Select(x =>ToDto(x)).ToArray();
            }
            
        }

        public HouseDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var item = baseService.GetAll().Include(x => x.Community).Include(x => x.Community.Region).Include(x => x.Community.Region.City).Include(x => x.Attachments).Include(x => x.HousePics).Include(x => x.RoomType).Include(x => x.Status).Include(x => x.DecorateStatus).Include(x => x.Type).SingleOrDefault(x => x.Id == id);
                if (item == null)
                {
                    throw new ArgumentException($"没有id={id}的相关数据");
                }
                return ToDto(item);
            }
        }

        public HouseDTO[] GetPagedData(long cityId,long roomTypeId,int pageSize, int currentIndex)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var datalist = baseService.GetAll().Include(x => x.Community).Include(x => x.Community.Region).Include(x => x.Community.Region.City).Include(x => x.Attachments).Include(x => x.HousePics).Include(x => x.RoomType).Include(x => x.Status).Include(x => x.DecorateStatus).Include(x => x.Type).Where(x => x.Community.Region.CityId == cityId&&x.RoomTypeId== roomTypeId).OrderBy(x => x.CreateDateTime).Skip(currentIndex).Take(pageSize);
                
                return datalist.ToList().Select(x => ToDto(x)).ToArray();
            }
        }

        //获取typeId这种房源类别下cityId这个城市中房源的总数量
        public long GetTotalCount(long cityId, long typeId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                return baseService.GetAll().Include(x => x.Community.Region.City).Where(x => x.Community.Region.CityId == cityId && x.TypeId == typeId).LongCount();
            }
        }


        public long Add(HouseDTO houseDTO)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                HouseEntity houseEntity = new HouseEntity();
                houseEntity.Address = houseDTO.Address;
                houseEntity.Area = houseDTO.Area;
                houseEntity.CheckInDateTime = houseDTO.CheckInDateTime;
                houseEntity.CommunityId = houseDTO.CommunityId;
                houseEntity.DecorateStatusId = houseDTO.DecorateStatusId;
                houseEntity.Description = houseDTO.Description;
                houseEntity.Direction = houseDTO.Direction;
                houseEntity.FloorIndex = houseDTO.FloorIndex;
                houseEntity.LookableDateTime = houseDTO.LookableDateTime;
                houseEntity.MonthRent = houseDTO.MonthRent;
                houseEntity.OwnerName = houseDTO.OwnerName;
                houseEntity.OwnerPhoneNum = houseDTO.OwnerPhoneNum;
                houseEntity.RoomTypeId = houseDTO.RoomTypeId;
                houseEntity.StatusId = houseDTO.StatusId;
                houseEntity.TotalFloorCount = houseDTO.TotalFloorCount;
                houseEntity.TypeId = houseDTO.TypeId;
                return baseService.AddItem(houseEntity);
            }
        }

        private HouseDTO ToDto(HouseEntity entity)
        {
            var houseDto = new HouseDTO() {
                Id = entity.Id,
                CreateTime = entity.CreateDateTime,
                CityId = entity.Community.Region.CityId,
                CityName = entity.Community.Region.City.Name,
                RegionId = entity.Community.RegionId,
                RegionName = entity.Community.Region.Name,
                CommunityId = entity.CommunityId,
                CommunityName = entity.Community.Name,
                CommunityLocation = entity.Community.Location,
                CommunityTraffic = entity.Community.Traffic,
                CommunityBuiltYear = entity.Community.BuiltYear,
                RoomTypeId = entity.RoomTypeId,
                RoomTypeName = entity.RoomType.Name,
                Address = entity.Address,
                MonthRent = entity.MonthRent,
                StatusId = entity.StatusId,
                StatusName = entity.Status.Name,
                Area = entity.Area,
                DecorateStatusId = entity.DecorateStatusId,
                DecorateStatusName = entity.DecorateStatus.Name,
                TotalFloorCount = entity.TotalFloorCount,
                FloorIndex = entity.FloorIndex,
                TypeId = entity.TypeId,
                TypeName = entity.Type.Name,
                Direction = entity.Direction,
                LookableDateTime = entity.LookableDateTime,
                CheckInDateTime = entity.CheckInDateTime,
                OwnerName = entity.OwnerName,
                OwnerPhoneNum = entity.OwnerPhoneNum,
                Description = entity.Description,
                AttachmentIds = entity.Attachments.Select(x => x.Id).ToArray(),
                //FirstThumbUrl = entity.HousePics.FirstOrDefault().ThumbUrl
            };
            return houseDto;
        }

        public void AddHousePic(long houseId, HousePicDTO housePicDTO)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var houseObj = baseService.GetById(houseId);
                HousePicEntity housePicEntity = new HousePicEntity();
                housePicEntity.HouseId = housePicDTO.HouseId;
                housePicEntity.Url = housePicDTO.Url;
                housePicEntity.ThumbUrl = housePicDTO.ThumbUrl;
                houseObj.HousePics.Add(housePicEntity);
                ctx.SaveChanges();
            }
        }


        public HousePicDTO[] ShowHousePic(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var housePicList = baseService.GetById(houseId).HousePics.Where(x=>x.IsDeleted==false).ToList();
                return housePicList.Select(x => new HousePicDTO
                {
                    Id = x.Id,
                    HouseId = x.HouseId,
                    Url = x.Url,
                    ThumbUrl = x.ThumbUrl,
                    CreateTime = x.CreateDateTime
                }).ToArray();
            }
        }
    }
}
