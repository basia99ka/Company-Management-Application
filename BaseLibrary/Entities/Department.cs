using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Department : BaseEntity 
    {
        //One to many 

        [JsonIgnore]
        public List<Branch>? Branches { get; set; }
        //[JsonIgnore]
        //public List<Employee>? Employees { get; set; }

    }
}
