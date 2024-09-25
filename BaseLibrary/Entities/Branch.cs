using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Branch : BaseEntity 
    {
        // many to one relathionship with department
        public Department? Department { get; set; }
        [Required]
        public int DepartmentId { get; set; }

        // one to many with employee
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        //one to many with project
        [JsonIgnore]
        public List<Project>? Project { get; set; }
 
    }
}
