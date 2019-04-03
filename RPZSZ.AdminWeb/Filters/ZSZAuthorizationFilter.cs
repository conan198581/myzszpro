using RPZSZ.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Filters
{
    public class ZSZAuthorizationFilter : IAuthorizationFilter
    {
        
        //public IAdminUserService AdminUserService { get; set; }

       
        
        

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            CheckAuthorizeAttribute[] permissionAttrs = (CheckAuthorizeAttribute[])filterContext.ActionDescriptor.GetCustomAttributes(typeof(CheckAuthorizeAttribute),false);
            //在action上没有CheckAuthorizeAttribute 标签属性
            if (permissionAttrs.Length <= 0)
            {
                return;
            }
            long? adminUserId = (long?)filterContext.HttpContext.Session["AdminUserId"];
            if (adminUserId == null)
            {
                //说明没有登录
                filterContext.Result = new ContentResult { Content = "没有登录" };
                return;
            }

            //如果登录了 就要开始判断了 该用户id 的权限 中 是否包含了 CheckAuthorizeAttribute 的所有标签属性
            //因为ZSZAuthorizationFilter 不是通过autofac生成的，所以也不会注入AdminUserService对象 自己手动获取
            IAdminUserService adminUserService = DependencyResolver.Current.GetService<IAdminUserService>();

            foreach (var permissionItem in permissionAttrs)
            {
                if (!(adminUserService.HasPermission(adminUserId.Value, permissionItem.Permission)))
                {
                    filterContext.Result = new ContentResult { Content = "没有相关权限" };
                    return;
                }
            }
            

        }
    }
}