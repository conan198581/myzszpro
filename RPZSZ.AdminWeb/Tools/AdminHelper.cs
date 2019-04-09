using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Tools
{
    public class AdminHelper
    {
        public static long? GetSessionAdminUserId(HttpContextBase httpContextBase)
        {
            return (long?)httpContextBase.Session["AdminUserId"];
        }
    }
}