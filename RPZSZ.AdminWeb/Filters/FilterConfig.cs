using RPZSZ.CommonMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPZSZ.AdminWeb.Filters
{
    public class FilterConfig
    {
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ZSZExceptionFilter());
            filters.Add(new JsonResultFilter());
            filters.Add(new ZSZAuthorizationFilter());
        }
    }
}