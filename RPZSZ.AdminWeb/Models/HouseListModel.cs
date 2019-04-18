using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class HouseListModel
    {
        public HouseDTO[] HouseList { get; set; }
        public long RoomTypeId { get; set; }
    }
}