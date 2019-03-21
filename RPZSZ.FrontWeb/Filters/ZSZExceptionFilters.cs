using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace RPZSZ.FrontWeb.Filters
{
    public class ZSZExceptionFilters : IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ZSZExceptionFilters));
        public void OnException(ExceptionContext filterContext)
        {
            log.Error("frontweb出现未处理的异常", filterContext.Exception);
        }
    }
}