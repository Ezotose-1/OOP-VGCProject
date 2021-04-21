using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    
    public class Course
    {
        public Course(){
            
        }
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int FacultyId { get; set; }

        [Required]
        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime StartingTime { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime EndingTime { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int GroupId { get; set; }
    }
}