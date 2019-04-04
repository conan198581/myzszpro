using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service
{
    public class RegionService : IRegionService
    {
        public long Add(long cityId, string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RegionEntity> baseService = new BaseService<RegionEntity>(ctx);
                return baseService.AddItem(new RegionEntity { Name = name, CityId = cityId });

            }
        }

        public RegionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RegionEntity> baseService = new BaseService<RegionEntity>(ctx);
                var entitylist = baseService.GetAll().Include(x => x.City).AsNoTracking().ToList();
                return entitylist.Select(x => ToDTO(x)).ToArray();
            }
        }

        public RegionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RegionEntity> baseService = new BaseService<RegionEntity>(ctx);
                var item = baseService.GetAll().Include(x => x.City).AsNoTracking().Where(x => x.Id == id).SingleOrDefault();
                if (item == null)
                {
                    throw new ArgumentException($"该{id}的region不存在");
                }
                return ToDTO(item);
            }
        }

        public RegionDTO ToDTO(RegionEntity entity)
        {
            var regiondto = new RegionDTO();
            regiondto.Id = entity.Id;
            regiondto.Name = entity.Name;
            regiondto.CreateTime = entity.CreateDateTime;
            regiondto.CityId = entity.CityId;
            regiondto.CityName = entity.City.Name;
            return regiondto;
        }

        public void Update(long id, string name, long cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RegionEntity> baseService = new BaseService<RegionEntity>(ctx);
                var entity = baseService.GetById(id);
                if (entity == null)
                {
                    throw new ArgumentException($"没有{id}的相关数据");
                }
                entity.Name = name;
                entity.CityId = cityId;
                ctx.SaveChanges();
            }

        }

        public RegionDTO[] GetRegionsByCityId(long cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RegionEntity> baseService = new BaseService<RegionEntity>(ctx);
                return baseService.GetAll().Where(x => x.CityId == cityId).ToList().Select(x => ToDTO(x)).ToArray();
            }
        }
    }
}
