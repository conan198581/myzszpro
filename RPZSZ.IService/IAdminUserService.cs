using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.DTO;

namespace RPZSZ.IService
{
    public interface IAdminUserService:IServiceSupport
    {
        long Add(string name, string phoneNum, string email,string password,long? cityId);
        bool CheckLogin(string phoneNUm, string password);
        AdminUserDTO[] GetAll();
        AdminUserDTO[] GetCityAdminUsers(long? cityId);
        AdminUserDTO GetById(long adminUserId);
        AdminUserDTO GetByPhoneNum(string phoneNum);
        bool HasPermission(long adminUserId, string permissionName);
        void UpdateAdminUser(long adminUserId, string name, string phoneNum, string email, string password, long? cityId);
        void MarkDeleted(long adminUserId);
        void RecordLoginError(long id);//记录错误登录一次
        void ResetLoginError(long id);//重置登录错误信息
        void UpdateRoleByAdminUserId(long userId, long[] roleIds);
        void BatchDelete(long[] ids);
    }
}
