using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class RegionEditGetModel
    {
        public RegionDTO RegionDTO { get; set; }
        public CityDTO[] CityDTOs { get; set; }
    }
}