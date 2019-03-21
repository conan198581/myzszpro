using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{
    public interface ICityService:IServiceSupport
    {
        //添加一个城市，如果存在同名，则抛出异常提示；
        long AddNewCity(string cityName);
    }
}
