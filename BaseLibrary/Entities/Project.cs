using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BaseLibrary.Entities
{
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public class Project : BaseEntity
    {
        public string? Description { get; set; } = string.Empty;
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public ProjectStatus projectStatus{ get; set; }
        
        // many to one relationship
        public Branch? Branch { get; set; }  
        public int BranchId { get; set; }


        public void CompletedProject()
    {
        if (projectStatus == ProjectStatus.Completed)
            {
                EndDate= DateTime.Now;
            }

    }
}
    
}
