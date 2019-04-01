using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RPZSZ.DTO;

namespace RPZSZ.AdminWeb.Models
{
    public class AdminUserAddGetModel
    {
        public List<CityDTO> Cities;
        public RoleDTO[] Roles;
    }
}