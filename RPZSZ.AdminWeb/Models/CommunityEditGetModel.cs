using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class CommunityEditGetModel
    {
        public CommunityDTO CommunityDTO { get; set; }
        public RegionDTO[] RegionDTOs { get; set; }
        public CityDTO[] CityDTOs { get; set; }
    }
}