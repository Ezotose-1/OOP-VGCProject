using Microsoft.AspNetCore.Mvc;
using OOP_VGCProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_VGCProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var courses = _context.Courses
                    .Include(c => c.Discipline);
            return View(await courses.ToListAsync());
        }

        // GET : Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                    .Include(c => c.Discipline)
                    .SingleOrDefaultAsync(x => x.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        private void PopulateDisciplineDropDownList(object selectedDiscipline = null)
        {
            var disciplineQuery = from d in _context.Discipline
                                  orderby d.CourseName
                                  select d;
            ViewBag.DisciplineId = new SelectList(disciplineQuery.AsNoTracking(), "DisciplineId", "CourseName", selectedDiscipline);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            PopulateDisciplineDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,CourseDescription,StartingTime,EndingTime,GroupId,DisciplineId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDisciplineDropDownList(course.DisciplineId);
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                                .AsNoTracking()
                                .SingleOrDefaultAsync(x => x.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            PopulateDisciplineDropDownList(course.DisciplineId);
            return View(course);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var courseToUpdate = await _context.Courses
                    .SingleOrDefaultAsync(c => c.CourseId == id);
            
            if (await TryUpdateModelAsync<Course>(courseToUpdate,
                "",
                c => c.CourseName, c => c.CourseDescription, 
                c => c.StartingTime, c => c.EndingTime, c => c.GroupId,
                c => c.DisciplineId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "contact your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDisciplineDropDownList(courseToUpdate.DisciplineId);
            return View(courseToUpdate);
        }

        // GET : Courses/Delete/5
        [Authorize(Roles = "Admin, Faculty")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                                .Include(c => c.Discipline)
                                .AsNoTracking()
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