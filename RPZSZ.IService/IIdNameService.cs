using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{
    public interface IIdNameService:IServiceSupport
    {
        long Add(IdNameDTO idNameDTO);
        IdNameDTO[] GetAll();
        IdNameDTO GetById(long id);
        void Update(IdNameDTO idNameDTO);
        void Delete(long id);
        IdNameDTO GetByName(string typeName, string name);
        IdNameDTO[] GetByTypeName(string typeName);
    }
}
