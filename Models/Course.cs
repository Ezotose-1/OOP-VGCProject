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
        public string CourseName { get; set; }
    }
}