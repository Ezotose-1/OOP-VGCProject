using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OOP_VGCProject.Models
{
    
    public class Course
    {
        public Course(){
            
        }
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string FacultyId { get; set; }

        [Required]
        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }

        [Required]
        [Display(Name = "Start")]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartingTime { get; set; }

        [Required]
        [Display(Name = "End")]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndingTime { get; set; }

        [Required]
        [Display(Name = "Group")]
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "DisciplineId")]
        public int? DisciplineId { get; set; }

        [Display(Name = "Discipline")]
        public virtual Discipline Discipline { get; set; }

        [Display(Name = "Faculty")]
        public virtual IdentityUser Faculty { get; set; }
    }
}