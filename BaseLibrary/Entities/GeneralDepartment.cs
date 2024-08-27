using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class GeneralDepartment:BaseEntity
    {
        // general department has many departments
        [JsonIgnore]
        public List<Department>departments { get; set; } = new();
    }
}
