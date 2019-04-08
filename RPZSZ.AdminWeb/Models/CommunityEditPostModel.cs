using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class CommunityEditPostModel
    {
        public long RegionId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Traffic { get; set; }
        public int? BuiltYear { get; set; }
        public long Id { get; set; }
    }
}