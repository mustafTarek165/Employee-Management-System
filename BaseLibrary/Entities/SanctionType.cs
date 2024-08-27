﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class SanctionType:BaseEntity
    {
        [JsonIgnore]
        public List<Sanction> sanctions {  get; set; } = new();
    }
}
