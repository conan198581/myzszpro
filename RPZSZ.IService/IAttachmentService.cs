﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.DTO;

namespace RPZSZ.IService
{
    public interface IAttachmentService : IServiceSupport
    {
        AttachmentDTO[] GetAll();
        AttachmentDTO[] GetAttachmentsByHouseId(long houseId);

        void UpdateAttachementsByHoueId(long houseId, long[] attachementIds);
    }
}
