﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service.Entities
{
    public class CityEntity :BaseEntity
    {
        public string Name { get; set; }
    }
}
