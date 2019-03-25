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
    public class AdminLogService : IAdminLogService
    {
        public long AddNew(long adminUserId, string message)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminLogEntity> baseService = new BaseService<AdminLogEntity>(ctx);
                var entity = new AdminLogEntity { AdminUserId=adminUserId,Message=message};
                return baseService.AddItem(entity);
            }
        }

        public AdminLogDTO GetById(long id)
        {

            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminLogEntity> baseService = new BaseService<AdminLogEntity>(ctx);
                var adminLogEntity = baseService.GetAll().Include(x => x.AdminUser).AsNoTracking().SingleOrDefault(x => x.Id == id);
                if (adminLogEntity == null)
                {
                    throw new ArgumentException($"没有这个id={id}的数据");
                }
                return ToDTO(adminLogEntity);
            }
        }

        public AdminLogDTO ToDTO(AdminLogEntity adminLogEntity)
        {
            var adminLogDto = new AdminLogDTO();
            adminLogDto.Id = adminLogEntity.Id;
            adminLogDto.Message = adminLogEntity.Message;
            adminLogDto.AdminUserId = adminLogEntity.AdminUserId;
            adminLogDto.CreateTime = adminLogEntity.CreateDateTime;
            adminLogDto.AdminUserName = adminLogEntity.AdminUser.Name;
            return adminLogDto;
        }
    }
}
