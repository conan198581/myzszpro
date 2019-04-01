using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class AdminUerAddPostModel
    {
        public string PhoneNum { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public long CityId { get; set; }
        public long[] roleIds { get; set; }
    }
}