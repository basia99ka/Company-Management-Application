using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string?  CivilId { get; set; } = string.Empty;
        //[Required]
        public string? FileName { get; set; } = string.Empty;
        //[Required]
        public string? Fullname { get; set; } = string.Empty;
        [Required]
        public string? JobName { get; set; } = string.Empty;
        [Required]
        public string? Address { get; set; } = string.Empty;

        [Required, DataType(DataType.PhoneNumber)]
        public string? TelephoneNumber { get; set; } = string.Empty;
        //[Required]
        public string? Photo { get; set; } = string.Empty;
        //[Required]
        public string? Other { get; set; }

        // relationship : Many to one

        //public Department? Department { get; set; }
        //public int DepartmentId { get; set; }
        public Branch? Branch { get; set; }

        [Required]
        public int BranchId { get; set; }

    }
}
