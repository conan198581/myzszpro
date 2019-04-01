using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPZSZ.AdminWeb.Models
{
    public class AdminUserEditGetModel
    {
        public AdminUserDTO AdminUserDTO { get; set; }
        public List<CityDTO> Cities { get; set; }
        public RoleDTO[] OwnRoleDTOs { get; set; }
        public RoleDTO[] RoleDTOs { get; set; }
    }
}