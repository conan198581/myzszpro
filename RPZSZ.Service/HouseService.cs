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

        public HouseDTO[] GetPagedData(long cityId, int pageSize, int currentIndex)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var datalist = baseService.GetAll().Include(x => x.Community).Include(x => x.Community.Region).Include(x => x.Community.Region.City).Include(x => x.Attachments).Include(x => x.HousePics).Include(x => x.RoomType).Include(x => x.Status).Include(x => x.DecorateStatus).Include(x => x.Type).Where(x => x.Community.Region.CityId == cityId).OrderBy(x => x.CreateDateTime).Skip(currentIndex).Take(pageSize);
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
                FirstThumbUrl = entity.HousePics.FirstOrDefault().ThumbUrl
            };
            return houseDto;
        }
    }
}
