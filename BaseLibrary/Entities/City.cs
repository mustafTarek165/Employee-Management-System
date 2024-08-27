using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class City:BaseEntity
    {
        //Many cities assigned to one country
        public Country? Country { get; set; } 
        public int CountryId {  get; set; }


        //One City has many towns
        [JsonIgnore]
        public List<Town> Towns { get; set;} = new();
    }
}
