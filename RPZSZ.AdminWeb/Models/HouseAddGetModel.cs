using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class HouseAddGetModel
    {
        //区域
        public RegionDTO[] Regions { get; set; }
        //房型
        public IdNameDTO[] Types { get; set; }
        //房源状态
        public IdNameDTO[] Status { get; set; }
        //装修状态
        public IdNameDTO[] DecorateStatus { get; set; }
        //配置设施
        public AttachmentDTO[] Attachments { get; set; }
    }
}