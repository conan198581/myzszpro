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
                    throw new ArgumentException($"{name}这个角色已经存在");
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

        public void Delete(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                baseService.MarkDelete(id);
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

        public RoleDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                return ToDTO(baseService.GetById(id));
            }
        }


        /// <summary>
        ///更新角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="name"></param>
        /// <param name="permissionIds"></param>
        public void UpdateRoleAndPermission(long roleId, string name, long[] permissionIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                BaseService<PermissionEntity> permissionService = new BaseService<PermissionEntity>(ctx);
                var existsList = baseService.GetAll().Where(x => x.Name == name);
                if (existsList.LongCount() > 1)
                {
                    throw new ArgumentException($"该{name}角色有多条记录重复，请联系管理员");
                }
                if (existsList.LongCount() == 1)
                {
                    var roleObj = existsList.SingleOrDefault();
                    if (roleObj.Id != roleId)
                    {
                        throw new ArgumentException($"该{name}角色已经存在,请重新选择别的角色名称");
                    }
                }
                var item = baseService.GetById(roleId);
                item.Name = name;
                var permissionList = permissionService.GetAll().Where(x => permissionIds.Contains(x.Id));
                //先删除后新增
                item.Permissions.Clear();
                foreach (var permissionObj in permissionList)
                {
                    item.Permissions.Add(permissionObj);
                }
                ctx.SaveChanges();
            }
        }

        public void BatchDelete(long[] ids)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> baseService = new BaseService<RoleEntity>(ctx);
                foreach (var id in ids)
                {
                    baseService.MarkDelete(id);
                }
            }
        }

        public RoleDTO[] GetRoleByUserId(long userId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> userService = new BaseService<AdminUserEntity>(ctx);
                return userService.GetById(userId).Roles.ToList().Select(x => ToDTO(x)).ToArray();
            }
        }
    }
}
