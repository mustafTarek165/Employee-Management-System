﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities.IdentityEntities
{
    public class SystemRole
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
