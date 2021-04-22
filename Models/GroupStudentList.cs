using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class GroupStudentList
    {
        [Key]
        public int AddId { get; set; }
        public int StudentId { get; set; }
        public string GroupId { get; set; }

    }
}
