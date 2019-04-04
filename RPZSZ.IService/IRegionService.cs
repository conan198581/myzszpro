using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{
    public interface IRegionService:IServiceSupport
    {
        RegionDTO[] GetAll();
        RegionDTO GetById(long id);
        long Add(long cityId, string name);

        void Update(long id, string name, long cityId);

        RegionDTO[] GetRegionsByCityId(long cityId);
    }
}
