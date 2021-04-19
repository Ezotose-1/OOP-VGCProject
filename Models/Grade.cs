using Microsoft.EntityFrameworkCore;

namespace OOP_VGCProject.Models
{
    public class Grades
    {
        public int Grade_id;
        public int Student_id;
        public decimal grade;
        public decimal coefficient;
        public string control_name;
        public string Disipline;
    }
}
