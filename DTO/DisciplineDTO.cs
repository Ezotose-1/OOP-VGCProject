using Microsoft.AspNetCore.Identity;
using OOP_VGCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.DTO
{
    public class DisciplineDTO
    {
        public Discipline discipline { get; set; }
        public List<UserDiscipline> UserIn { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}
