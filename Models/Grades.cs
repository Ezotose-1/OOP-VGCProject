using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OOP_VGCProject.Models
{
    public class Grades
    {
        [Key]
        public int Grade_id { get; set; }
        public string Student_id { get; set; }
        public decimal grade { get; set; }
        public decimal coefficient { get; set; }
        public string control_name { get; set; }
        public string Disipline { get; set; }
    }
}
