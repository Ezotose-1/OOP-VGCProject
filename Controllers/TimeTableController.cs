using Microsoft.AspNetCore.Mvc;
using OOP_VGCProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_VGCProject.Controllers
{
    public class TimeTableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeTableController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<string> Titles = new List<string>() { "cours 1","cours 2","cours 3","cours 4","cours 5"};
            List<string> Descriptions = new List<string>() { "bullshit 1", "bullshit 2", "bullshit 3", "bullshit 4", "bullshit 5" };
            List<DateTime> StartingDates = new List<DateTime>() { DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-2), DateTime.Now, DateTime.Now, DateTime.Now };
            List<DateTime> EndingDates = new List<DateTime>() { DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now };
            List<string> ThemesColors = new List<string>() { "FF0000", "00FF0F", "FB0F0", "FF0DD0", "CCBB00" };

            ViewBag.EventTitles = Titles;
            ViewBag.EventDescriptions = Descriptions;
            ViewBag.EventStartingDates = StartingDates;
            ViewBag.EventEndingDates = EndingDates;
            ViewBag.EventThemesColors = ThemesColors;
            return View();
        }


        public void DisplayEvents()
        {
            throw new NotImplementedException();

        }
    }
}
