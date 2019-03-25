﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPZSZ.DTO;

namespace RPZSZ.IService
{
    public interface ICommunityService:IServiceSupport
    {
        CommunityDTO[] GetCommunityByRegionId(long regionId);
    }
}