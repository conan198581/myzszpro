using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class HouseAddPostModel
    {
        public long RoomTypeId { get; set; }
        public long RegionId { get; set; }
        public long CommunityId { get; set; }
        public long TypeId { get; set; }
        public string Address { get; set; }
        public int MonthRent { get; set; }
        public long StatusId { get; set; }
        public decimal Area { get; set; }
        public long DecorateStatusId { get; set; }
        public int FloorIndex { get; set; }
        public int TotalFloorCount { get; set; }
        public string Direction { get; set; }
        public DateTime lookableDateTime { get; set; }
        public DateTime CheckInDateTime { get; set; }
        public string ownerPhoneNum { get; set; }
        public string OwnerName { get; set; }
        public string description { get; set; }
        public long[] attachmentIds { get; set; }
    }
}