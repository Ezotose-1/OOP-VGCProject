using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.DTO
{
    public class ExamsDTO
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public DateTime Date { get; set; }
        public string Course { get; set; }
    }
}
