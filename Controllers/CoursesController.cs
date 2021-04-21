using Microsoft.AspNetCore.Mvc;
using OOP_VGCProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_VGCProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace OOP_VGCProject.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET : Courses
        public async Task<IActionResult> Index()
        {
            var courses = _context.Courses;
            return View(await courses.ToListAsync());
        }


        // GET : Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var course = await _context.Courses
                    .SingleOrDefaultAsync(x => x.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // GET : Courses/Delete/5
        [Authorize(Roles = "Admin, Faculty")]
        public async Task<IActionResult> Delete(int? id)
        {
            var course = await _context.Courses
                                .SingleOrDefaultAsync(x => x.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST : Courses/Delete/5
        [Authorize(Roles = "Admin, Faculty")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}