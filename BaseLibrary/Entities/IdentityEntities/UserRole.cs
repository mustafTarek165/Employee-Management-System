﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities.IdentityEntities
{
    public class UserRole
    {
        [Key]
        public int Id {  get; set; }    
        public int UserId { get; set; }
        public int RoleId { get; set; } 
    }
}
