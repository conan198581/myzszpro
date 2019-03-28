using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{
    public interface IRoleService:IServiceSupport
    {
        RoleDTO[] GetAll();

        long Add(string name,long[] permissionIds);
    }
}
