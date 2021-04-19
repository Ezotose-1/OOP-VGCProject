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

        public static IQueryable<IdentityUser> AspNetUsers { get; internal set; }
        //public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Event> Event { get; set; }

    }
}
