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
            
            List<string> Titles = new List<string>();
            List<string> Descriptions = new List<string>();
            List<DateTime> StartingDates = new List<DateTime>();
            List<DateTime> EndingDates = new List<DateTime>();
            List<string> ThemesColors = new List<string>();

            string userId =  _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var groupsListsQ =  await _context.GroupStudentList.Where(x => x.StudentId == userId).ToListAsync();

            
            List<string> groupIdWhereUserIs = new List<string>();
            foreach(var group in groupsListsQ)
            {
                groupIdWhereUserIs.Add(group.GroupId);
            }

            var coursesQ = await _context.Courses.Where(x => groupIdWhereUserIs.Contains(x.GroupId)).ToListAsync();
            

            foreach(var cours in coursesQ)
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
