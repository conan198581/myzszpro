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
    public class PermissionService : IPermissionService
    {
        public long Add(string name, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                PermissionEntity permissionEntity = new PermissionEntity() { Name = name, Description = description };
                return baseService.AddItem(permissionEntity);
            }
        }

        public void BatchDel(long[] ids)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                foreach (var id in ids)
                {
                    baseService.MarkDelete(id);
                }
            }
        }

        public int Edit(long id, string name, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                var entity = baseService.GetById(id);
                entity.Name = name;
                entity.Description = description;
                return ctx.SaveChanges();
            }
        }

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                return baseService.GetAll().ToList().Select(x => ToDTO(x)).ToArray();
                
            }
        }

        public PermissionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                return ToDTO(baseService.GetById(id));
            }
        }

        public PermissionDTO[] GetByRoleId(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                return baseService.GetById(roleId).Permissions.ToList().Select(x => ToDTO(x)).ToArray();
            }
        }

        public PermissionDTO ToDTO(PermissionEntity permissionEntity)
        {
            var permissionDto = new PermissionDTO();
            permissionDto.Id = permissionEntity.Id;
            permissionDto.Name = permissionEntity.Name;
            permissionDto.Description = permissionEntity.Description;
            permissionDto.CreateTime = permissionEntity.CreateDateTime;
            return permissionDto;
        }
    }
}
