using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.DTO;
using System.Data.Entity;

namespace RPZSZ.Service
{
    public class HouseAppointmentService : IHouseAppointmentService
    {
        public long AddNew(long? userId, string name, string phoneNum, long houseId, DateTime visitData)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseAppointmentEntity> baseService = new BaseService<HouseAppointmentEntity>(ctx);
                HouseAppointmentEntity entity = new HouseAppointmentEntity {
                    UserId = userId,
                    Name = name,
                    PhoneNum = phoneNum,
                    HouseId = houseId,
                    VisitDate = visitData
                };
                return baseService.AddItem(entity);
            }
        }

        public HouseAppointmentDTO GetHouseAppointmentById(long houseAppoimentId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseAppointmentEntity> baseService = new BaseService<HouseAppointmentEntity>(ctx);
                var item = baseService.GetAll().Include(x => x.House).Include(x => x.FollowAdminUser).Include(x => x.House.Community).Include(x => x.House.Community.Region).AsNoTracking().SingleOrDefault(x => x.Id == houseAppoimentId);
                if (item == null)
                {
                    return null;
                }
                return ToDTO(item);

            }
        }


        public HouseAppointmentDTO[] GetPageData(long cityId, string status, int startIndex, int pageSize)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseAppointmentEntity> baseService = new BaseService<HouseAppointmentEntity>(ctx);
                var datalist = baseService.GetAll().Include(x => x.House).Include(x => x.FollowAdminUser).Include(x => x.House.Community).Include(x => x.House.Community.Region).AsNoTracking().Where(x => x.House.Community.Region.City.Id == cityId && x.Status == status).OrderByDescending(x => x.CreateDateTime).Skip(startIndex).Take(pageSize);
                //tolist主要是为了防止延迟加载，一旦延迟加载，当去读取数据的时候 dbcontext就已经销毁了；
                return datalist.ToList().Select(x => ToDTO(x)).ToArray();
            }
        }

        public long GetTotalCount(long cityId, string status)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseAppointmentEntity> baseService = new BaseService<HouseAppointmentEntity>(ctx);
                return baseService.GetAll().Include(x => x.House.Community.Region).Where(x => x.Status == status && x.House.Community.Region.CityId == cityId).LongCount();
            }
        }


        private HouseAppointmentDTO ToDTO(HouseAppointmentEntity houseApp)
        {
            HouseAppointmentDTO dto = new HouseAppointmentDTO();
            dto.CommunityName = houseApp.House.Community.Name;
            dto.CreateTime = houseApp.CreateDateTime;
            dto.FollowAdminUserId = houseApp.FollowAdminUserId;
            if (houseApp.FollowAdminUser != null)
            {
                dto.FollowAdminUserName = houseApp.FollowAdminUser.Name;
            }
            dto.FollowDateTime = houseApp.FollowDateTime;
            dto.HouseId = houseApp.HouseId;
            dto.Id = houseApp.Id;
            dto.Name = houseApp.Name;
            dto.PhoneNum = houseApp.PhoneNum;
            dto.RegionName = houseApp.House.Community.Region.Name;
            dto.Status = houseApp.Status;
            dto.UserId = houseApp.UserId;
            dto.VisitDate = houseApp.VisitDate;
            dto.HouseAddress = houseApp.House.Address;
            return dto;
        }

    }
}
