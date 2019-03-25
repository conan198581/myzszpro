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
                return baseService.GetAll().AsNoTracking().Where(x => x.RegionId == regionId).Select(x => new CommunityDTO { Name = x.Name, RegionId = x.RegionId, Location = x.Location, Traffic = x.Traffic, BuiltYear = x.BuiltYear,Id=x.Id,CreateTime=x.CreateDateTime }).ToArray();
            }
        }
    }
}
