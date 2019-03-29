using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.DTO;

namespace RPZSZ.IService
{
    public interface IPermissionService:IServiceSupport
    {
        PermissionDTO[] GetAll();

        long Add(string name, string description);

        PermissionDTO GetById(long id);

        int Edit(long id, string name, string description);

        void BatchDel(long[] ids);

        PermissionDTO[] GetByRoleId(long roleId);
    }
}
