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

    /// <summary>
    /// 房子的配套设施
    /// </summary>
    public class AttachmentService : IAttachmentService
    {

        //获取所有的配套设施
        public AttachmentDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AttachmentEntity> baseService = new BaseService<AttachmentEntity>(ctx);
                return baseService.GetAll().Select(x => ToDTO(x)).ToArray();
            }
        }

        #region 获取一个房子下面的 配套设施 GetAttachmentsByHouseId(long houseId)
        /// <summary>
        /// 获取一个房子下面的 配套设施
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public AttachmentDTO[] GetAttachmentsByHouseId(long houseId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<HouseEntity> baseService = new BaseService<HouseEntity>(ctx);
                var houseObj = baseService.GetAll().Include(x=>x.Attachments).AsNoTracking().SingleOrDefault(x => x.Id == houseId);
                if (houseObj == null)
                {
                    throw new Exception($"house id {houseId} 该房子不存在");
                }
                var attachments = houseObj.Attachments.Select(x => ToDTO(x)).ToArray();
                return attachments;
            }
        } 
        #endregion

        private AttachmentDTO ToDTO(AttachmentEntity attachmentEntity)
        {
            AttachmentDTO attachmentDTO = new AttachmentDTO();
            attachmentDTO.Name = attachmentEntity.Name;
            attachmentDTO.IconName = attachmentEntity.IconName;
            attachmentDTO.Id = attachmentEntity.Id;
            attachmentDTO.CreateTime = attachmentEntity.CreateDateTime;
            return attachmentDTO;
        }
    }
}
