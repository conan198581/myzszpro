using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class RoleEditViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PermissionDTO[] OwnPermissionDTOs { get; set; }
        public PermissionDTO[] PermissionDTOs { get; set; }
    }
}