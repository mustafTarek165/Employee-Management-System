using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Branch:BaseEntity
    {
        //many branches assigned to one department
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        //one branch has many employees
        [JsonIgnore]
        public List<Employee> Employees { get; set; } = new();
    }
}
