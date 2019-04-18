using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{
    public interface IHouseService : IServiceSupport
    {
        HouseDTO[] GetAll();
        HouseDTO GetById(long id);
        //获取typeId这种房源类别下cityId这个城市中房源的总数量
        long GetTotalCount(long cityId, long typeId);

        HouseDTO[] GetPagedData(long cityId,long roomTypeId,int pageSize, int currentIndex);
        long Add(HouseDTO houseDTO);
    }
}
