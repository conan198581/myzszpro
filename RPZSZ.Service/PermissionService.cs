using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
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

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> baseService = new BaseService<PermissionEntity>(ctx);
                return baseService.GetAll().ToList().Select(x => ToDTO(x)).ToArray();
                
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
