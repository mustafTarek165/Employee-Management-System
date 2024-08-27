using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Employee:BaseEntity
    {
       
      
        [Required]
        public string CivilId {  get; set; }=String.Empty;
     
        [Required]

        public string FileNumber { get; set; } = String.Empty;
   
        
      
        [Required,DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; } = String.Empty;
        [Required]
        public string JobName { get; set; } = String.Empty;
        [Required]
        public string Address { get; set; } = String.Empty;
        [Required]
        public string Photo { get; set; } = String.Empty;
 
        public string? Other { get; set; }

        //many Employees assigned to one Town
        public int? TownId { get; set; }
        public Town? Town { get; set; } 

        //many Employees assigned to one Branch
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; } 

    }
}
