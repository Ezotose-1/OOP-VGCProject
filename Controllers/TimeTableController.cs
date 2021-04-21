using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOP_VGCProject.Data;
using OOP_VGCProject.DTO;
using OOP_VGCProject.Models;

namespace OOP_VGCProject.Controllers
{
    public class TimeTableController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public TimeTableController(ApplicationDbContext context,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var Cours = _context.Courses.ToList();
           
            List<string> Titles = new List<string>();
            List<string> Descriptions = new List<string>();
            List<DateTime> StartingDates = new List<DateTime>();
            List<DateTime> EndingDates = new List<DateTime>();
            List<string> ThemesColors = new List<string>();

            foreach(var cours in Cours)
            {
                Titles.Add(cours.CourseName);
                Descriptions.Add(cours.CourseDescription);
                StartingDates.Add(cours.StartingTime);
                EndingDates.Add(cours.EndingTime);
            }

            ViewBag.EventTitles = Titles;
            ViewBag.EventDescriptions = Descriptions;
            ViewBag.EventStartingDates = StartingDates;
            ViewBag.EventEndingDates = EndingDates;

            return View();
        }


        public void DisplayEvents()
        {
            throw new NotImplementedException();

        }
    }
}
