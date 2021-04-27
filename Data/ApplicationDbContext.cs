using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOP_VGCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_VGCProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OOP_VGCProject.Models.Fees> Fees { get; set; }
        public DbSet<OOP_VGCProject.Models.Grades> Grades { get; set; }
        public DbSet<OOP_VGCProject.Models.UserFee> UserFee { get; set; }
        public DbSet<OOP_VGCProject.Models.Exams> Exams { get; set; }
        public DbSet<OOP_VGCProject.Models.Discipline> Discipline { get; set; }

        public DbSet<OOP_VGCProject.Models.UserDiscipline> UserDiscipline { get; set; }
        public DbSet<OOP_VGCProject.Models.GroupStudentList> GroupStudentList { get; set; }
        public DbSet<OOP_VGCProject.Models.Course> Courses { get; set; }

    }
}