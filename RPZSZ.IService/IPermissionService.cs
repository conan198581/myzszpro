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
    }
}
