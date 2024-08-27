using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Sanction:OtherBaseEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Punishment { get; set; } = string.Empty;
        [Required]
        public DateTime PunishmentDate { get; set; }    
        //many to one relationship with vacation type
        public SanctionType? SanctionType { get; set; }

        [Required]
        public int  SanctionTypeId { get; set;}
    }
}
