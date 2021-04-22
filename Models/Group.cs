using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class Group
    {
        [Key]
        public string GroupId { get; set; }
        public string FacultyId { get; set; }
        public int GroupListId { get; set; }
    }
}
