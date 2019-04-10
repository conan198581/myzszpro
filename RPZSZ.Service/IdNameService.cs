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
    public class IdNameService : IIdNameService
    {
        public long Add(IdNameDTO idNameDTO)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> idnameservice = new BaseService<IdNameEntity>(ctx);
                return idnameservice.AddItem(new IdNameEntity { Name = idNameDTO.Name, TypeName = idNameDTO.TypeName });
            }
        }

        public void Delete(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> service = new BaseService<IdNameEntity>(ctx);
                service.MarkDelete(id);
            }
        }

        public IdNameDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> service = new BaseService<IdNameEntity>(ctx);
                return service.GetAll().ToList().Select(x => ToDTO(x)).ToArray();
            }
        }

        public IdNameDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> service = new BaseService<IdNameEntity>(ctx);
                return ToDTO(service.GetById(id));
            }
        }

        public IdNameDTO GetByName(string typeName, string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> service = new BaseService<IdNameEntity>(ctx);
                var item = service.GetAll().Where(x => x.TypeName == typeName && x.Name == name).SingleOrDefault();
                if (item == null)
                {
                    throw new Exception($"没有这种{typeName}类型的{name}名称");
                }
                return ToDTO(item);
                
            }
        }

        public IdNameDTO[] GetByTypeName(string typeName)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> baseService = new BaseService<IdNameEntity>(ctx);
                return baseService.GetAll().Where(x => x.TypeName == typeName).ToList().Select(x => ToDTO(x)).ToArray();
            }
        }

        public void Update(IdNameDTO idNameDTO)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<IdNameEntity> service = new BaseService<IdNameEntity>(ctx);
                var item = service.GetById(idNameDTO.Id);
                item.Name = idNameDTO.Name;
                item.TypeName = idNameDTO.TypeName;
                ctx.SaveChanges();
            }
        }

        private IdNameDTO ToDTO(IdNameEntity idNameEntity)
        {
            var idnamedto = new IdNameDTO();
            idnamedto.Id = idNameEntity.Id;
            idnamedto.Name = idNameEntity.Name;
            idnamedto.TypeName = idNameEntity.TypeName;
            idnamedto.CreateTime = idNameEntity.CreateDateTime;
            return idnamedto;
        }
    }
}
