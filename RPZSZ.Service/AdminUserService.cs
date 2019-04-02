using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.Common;
using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;

namespace RPZSZ.Service
{
    public class AdminUserService : IAdminUserService
    {

        /// <summary>
        /// 添加 一个 adminuser
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phoneNum"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public long Add(string name, string phoneNum, string email,string password,long? cityId)
        {
            string passwordSalt = CommonHelper.CreateVerifyCode(5);
            string securityPassword = CommonHelper.CalcMD5(password + passwordSalt);
            AdminUserEntity adminUserEntity = new AdminUserEntity() {
                Name = name,
                PhoneNum = phoneNum,
                Email = email,
                PasswordSalt = passwordSalt,
                PasswordHash = securityPassword,
                CityId = cityId
            };
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                if (baseService.GetAll().Any(x => x.PhoneNum == phoneNum))
                {
                    throw new ArgumentException("手机号码已经存在" + phoneNum);
                }
                return baseService.AddItem(adminUserEntity);
            }
        }

        /// <summary>
        /// 通过手机号 和 密码 验证登录
        /// </summary>
        /// <param name="phoneNUm"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckLogin(string phoneNUm, string password)
        {
            using (ZSZDbContext cxt = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(cxt);
                var adminUserItem = baseService.GetAll().Where(x => x.PhoneNum == phoneNUm).SingleOrDefault();
                if (adminUserItem == null)
                {
                    return false;
                }
                var salt = adminUserItem.PasswordSalt;
                var securityPassword = CommonHelper.CalcMD5(password + salt);
                if (adminUserItem.PasswordHash == securityPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        #region 查询所有的AdminUser GetAll()
        public AdminUserDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                var adminUserList = ctx.AdminUsers.Where(x => x.IsDeleted == false).Include(c => c.City).AsNoTracking().Select(x => new AdminUserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNum = x.PhoneNum,
                    Email = x.Email,
                    CreateTime = x.CreateDateTime,
                    CityId = x.CityId,
                    CtiyName = x.CityId==null?"总部":x.City.Name,
                    LoginErrorTimes = x.LoginErrorTimes,
                    LastLoginErrorDateTime = x.LastLoginErrorDateTime
                });
                return adminUserList.ToArray();
            }
        }
        #endregion


        #region 通过adminUserId查询记录 GetById(long adminUserId)
        public AdminUserDTO GetById(long adminUserId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                var item = ctx.AdminUsers.Where(x => x.IsDeleted == false && x.Id == adminUserId).Include(x => x.City).AsNoTracking().SingleOrDefault();
                if (item == null)
                {
                    throw new Exception($"没有相关的{adminUserId}数据");
                }
                return ToDTO(item);
            }
        }
        #endregion

        #region 通过手机号查询单个记录 GetByPhoneNum(string phoneNum)
        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                var adminUserlist = baseService.GetAll().Include(x=>x.City).AsNoTracking().Where(x => x.PhoneNum == phoneNum);
                var count = adminUserlist.Count();
                if (count<= 0)
                {
                    return null;
                }
                else if (count > 2)
                {
                    throw new Exception($"找到有多个手机号为{phoneNum}的记录，请联系管理员");
                }
                else
                {
                    var adminUserItem = adminUserlist.SingleOrDefault();
                    return ToDTO(adminUserItem);
                }
            }
        } 
        #endregion

        public AdminUserDTO[] GetCityAdminUsers(long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                return baseService.GetAll().Include(x => x.City).AsNoTracking().Where(x => x.CityId == cityId).Select(x => ToDTO(x)).ToArray();
            }
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                var adminUserItem = baseService.GetAll().Include(x => x.Roles).AsNoTracking().Where(x => x.Id == adminUserId).SingleOrDefault();
                if (adminUserItem != null)
                {
                    var rolelist = adminUserItem.Roles;
                    return rolelist.SelectMany(r => r.Permissions).Any(x => x.Name == permissionName);
                }
                else
                {
                    throw new ArgumentException("找不到id=" + adminUserId + "的用户");
                }
            }
        }

        public void MarkDeleted(long adminUserId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                baseService.MarkDelete(adminUserId);
            }
        }

        public void UpdateAdminUser(long adminUserId, string name, string phoneNum, string email, string password, long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                var item = baseService.GetById(adminUserId);
                item.Name = name;
                item.PhoneNum = phoneNum;
                item.Email = email;
                if (password != null)
                {
                    string salt = CommonHelper.CreateVerifyCode(5);
                    string securityPassword = CommonHelper.CalcMD5(password + salt);
                    item.PasswordSalt = salt;
                    item.PasswordHash = securityPassword;
                }
                item.CityId = cityId;
                ctx.SaveChanges();  
            }
        }

        private AdminUserDTO ToDTO(AdminUserEntity adminUserEntity)
        {
            var adminUserDto = new AdminUserDTO();
            adminUserDto.Id = adminUserEntity.Id;
            adminUserDto.Name = adminUserEntity.Name;
            adminUserDto.PhoneNum = adminUserEntity.PhoneNum;
            adminUserDto.Email = adminUserEntity.Email;
            adminUserDto.CreateTime = adminUserEntity.CreateDateTime;
            adminUserDto.CityId = adminUserEntity.CityId;
            if(adminUserEntity.City != null)
            {
                adminUserDto.CtiyName = adminUserEntity.City.Name;//需要Include提升性能
                //如鹏总部（北京）、如鹏网上海分公司、如鹏广州分公司、如鹏北京分公司
            }
            else
            {
                adminUserDto.CtiyName = "总部";
            }
            //adminUserDto.CtiyName = adminUserEntity.City.Name;
            adminUserDto.LoginErrorTimes = adminUserEntity.LoginErrorTimes;
            adminUserDto.LastLoginErrorDateTime = adminUserEntity.LastLoginErrorDateTime;
            return adminUserDto;
        }



        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoleByAdminUserId(long userId, long[] roleIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> baseService = new BaseService<AdminUserEntity>(ctx);
                var userEntity = baseService.GetById(userId);
                userEntity.Roles.Clear();
                BaseService<RoleEntity> roleService = new BaseService<RoleEntity>(ctx);
                var roleList = roleService.GetAll().Where(x => roleIds.Contains(x.Id));
                foreach (var item in roleList)
                {
                    userEntity.Roles.Add(item);
                }
                ctx.SaveChanges();
            }
        }

        public void BatchDelete(long[] ids)
        {
            foreach (var id in ids)
            {
                MarkDeleted(id);
            }
        }
    }
}
