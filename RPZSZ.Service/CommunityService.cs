using RPZSZ.DTO;
using RPZSZ.IService;
using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RPZSZ.Service
{
    public class CommunityService : ICommunityService
    {
        public long Add(CommunityDTO communityDTO)
        {
            var entity = new CommunityEntity {
                Name=communityDTO.Name,
                RegionId =communityDTO.RegionId,
                Location = communityDTO.Location,
                Traffic = communityDTO.Traffic,
                BuiltYear = communityDTO.BuiltYear
            };
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> baseService = new BaseService<CommunityEntity>(ctx);
                return baseService.AddItem(entity);
            }
        }

        public CommunityDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> baseService = new BaseService<CommunityEntity>(ctx);
                return baseService.GetAll().Include(x=>x.Region).AsNoTracking().ToList().Select(x => ToDTO(x)).ToArray();//这块要是不事先List会出现延迟加载的情况；
            }
        }

        public CommunityDTO GetById(long communityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> baseService = new BaseService<CommunityEntity>(ctx);
                var dtoItem = baseService.GetAll().Include(x => x.Region).Include(y=>y.Region.City).AsNoTracking().Where(x => x.Id == communityId).SingleOrDefault();
                return ToDTO(dtoItem);
            }
        }



        /// <summary>
        /// 通过地区id 获取该地区下的所有社区
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public CommunityDTO[] GetCommunityByRegionId(long regionId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> baseService = new BaseService<CommunityEntity>(ctx);
                return baseService.GetAll().AsNoTracking().Where(x => x.RegionId == regionId).Select(x => new CommunityDTO { Name = x.Name, RegionId = x.RegionId, Location = x.Location, Traffic = x.Traffic, BuiltYear = x.BuiltYear, Id = x.Id, CreateTime = x.CreateDateTime }).ToArray();
            }
        }

        public void Update(CommunityDTO communityDTO)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<CommunityEntity> baseService = new BaseService<CommunityEntity>(ctx);
                var item = baseService.GetById(communityDTO.Id);
                item.Name = communityDTO.Name;
                item.RegionId = communityDTO.RegionId;
                item.Location = communityDTO.Location;
                item.Traffic = communityDTO.Traffic;
                item.BuiltYear = communityDTO.BuiltYear;
                ctx.SaveChanges();
            }
        }

        private CommunityDTO ToDTO(CommunityEntity communityEntity)
        {
            var communityDto = new CommunityDTO();
            communityDto.Id = communityEntity.Id;
            communityDto.Name = communityEntity.Name;
            communityDto.RegionId = communityEntity.RegionId;
            communityDto.RegionName = communityEntity.Region.Name;
            communityDto.Location = communityEntity.Location;
            communityDto.Traffic = communityEntity.Traffic;
            communityDto.BuiltYear = communityEntity.BuiltYear;
            communityDto.CreateTime = communityEntity.CreateDateTime;
            communityDto.CityId = communityEntity.Region.CityId;
            communityDto.CityName = communityEntity.Region.City.Name;
            return communityDto;
        }
    }
}
