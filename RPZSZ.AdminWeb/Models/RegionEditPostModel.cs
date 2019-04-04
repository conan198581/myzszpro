using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class RegionEditPostModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
}