using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service.Entities
{
    public class AttachmentEntity :BaseEntity
    {
        public string Name { get; set; }
        public string IconName { get; set; }

        public virtual ICollection<HouseEntity> Houses { get; set; } = new List<HouseEntity>();
    }
}
