using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Filters
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=true)]
    public class CheckAuthorizeAttribute:Attribute
    {
        public string Permission { get; set; }

        public CheckAuthorizeAttribute(string permission)
        {
            this.Permission = permission;
        }
    }
}