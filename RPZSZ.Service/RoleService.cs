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
    public class RoleService : IRoleService
    {
        public RoleDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                return baseService.GetAll().ToList().Select(x => ToDTO(x)).ToArray();
            }
        }

        public long Add(string name, long[] permissionIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                if (baseService.GetAll().Any(x => x.Name == name))
                {
                    throw new ArgumentException($"{name}这个权限已经存在");
                }
                RoleEntity roleEntity = new RoleEntity { Name = name };
                long roleId = baseService.AddItem(roleEntity);
                BaseService<PermissionEntity> permissionService = new BaseService<PermissionEntity>(ctx);
                var permissionList = permissionService.GetAll().Where(x => permissionIds.Contains(x.Id));
                foreach (var permissionObj in permissionList)
                {
                    roleEntity.Permissions.Add(permissionObj);
                }
                ctx.SaveChanges();
                return roleId;
            }
        }

        private RoleDTO ToDTO(RoleEntity roleEntity)
        {
            var roleDto = new RoleDTO();
            roleDto.Id = roleEntity.Id;
            roleDto.CreateTime = roleEntity.CreateDateTime;
            roleDto.Name = roleEntity.Name;
            return roleDto;
        }
    }
}
