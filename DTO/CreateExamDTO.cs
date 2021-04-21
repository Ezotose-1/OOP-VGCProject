using OOP_VGCProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.DTO
{
    public class CreateExamDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ExamName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Course { get; set; }
        public List<Discipline> disciplines { get; set; }
    }
}
