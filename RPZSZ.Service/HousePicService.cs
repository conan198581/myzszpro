using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service
{
    public class HousePicService : IHousePicService
    {
        public void Delete(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HousePicEntity> baseService = new BaseService<HousePicEntity>(ctx);
                baseService.MarkDelete(id);
            }
        }
    }
}
