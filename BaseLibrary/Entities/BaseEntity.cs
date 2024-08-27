using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Valid Name")]
        public string Name { get; set; } = string.Empty;
      /*  [JsonIgnore]
        public List<Employee>? Employees { get; set; }*/
    }
}
