using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Models
{
    public class UserDiscipline
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int DisciplineId { get; set; }
    }
}
