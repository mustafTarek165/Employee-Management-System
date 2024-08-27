using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities.IdentityEntities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]  
        public string? Password { get; set; }
    }
}
