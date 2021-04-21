using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class Exams
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ExamName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
