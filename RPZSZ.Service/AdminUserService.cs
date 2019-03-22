using System;
using System.Collections.Generic;
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
            string securityPassword = CommonHelper.CalcMD5(passwordSalt+passwordSalt);
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

        public AdminUserDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO GetById(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO[] GetCityAdminUsers(long? cityId)
        {
            throw new NotImplementedException();
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long adminUserId, string name, string phoneNum, string email, string password, long? cityId)
        {
            throw new NotImplementedException();
        }
    }
}
