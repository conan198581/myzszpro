using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service
{
    public class CityService : ICityService
    {
        public long AddNewCity(string cityName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CityEntity> baseService = new BaseService<CityEntity>(ctx);
                if (baseService.GetAll().Any(x => x.Name == cityName))
                {
                    throw new Exception("已经存在相同名称的城市");
                }
                CityEntity cityEntity = new CityEntity() { Name = cityName };
                return baseService.AddItem(cityEntity);
            }
        }

        public CityDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CityEntity> baseService = new BaseService<CityEntity>(ctx);
                return baseService.GetAll().ToList().Select(x=>ToDTO(x)).ToArray();
            }
        }

        public CityDTO ToDTO(CityEntity cityEntity)
        {
            var citydto = new CityDTO();
            citydto.Id = cityEntity.Id;
            citydto.Name = cityEntity.Name;
            citydto.CreateTime = cityEntity.CreateDateTime;
            return citydto;
        }
    }
}
