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
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        public ExamsController(ApplicationDbContext context,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<IdentityUser> userManager)
        {
            _context = context;

            _roleManager = roleManager;
            _userManager = userManager;

        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var Exams = await _context.Exams.ToListAsync();
            
            bool isAdmin = User.IsInRole("Admin");
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            
            var ExamLst = new List<ExamsDTO>();
            foreach (var e in Exams)
            {
                if (isAdmin || _context.UserDiscipline.ToList().Exists(d => d.DisciplineId == e.CourseId && d.UserId == user.Id)) {
                    ExamLst.Add(new ExamsDTO
                    {
                        Id = e.Id,
                        ExamName = e.ExamName,
                        Date = e.Date,
                        Course = _context.Discipline.ToList().Find(c => c.Id == e.CourseId).CourseName
                    });
                }
            }
            return View(ExamLst);
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exams = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exams == null)
            {
                return NotFound();
            }

            return View(exams);
        }

        // GET: Exams/Create
        [Authorize(Roles = "Admin, Faculty")]
        public IActionResult Create()
        {
            var model = new CreateExamDTO
            {
                disciplines = _context.Discipline.ToList()
            };
            return View(model);
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Faculty")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExamName,Date,CourseId")] Exams exams)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exams);
        }

        // GET: Exams/Edit/5
        [Authorize(Roles = "Admin, Faculty")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exams = await _context.Exams.FindAsync(id);
            if (exams == null)
            {
                return NotFound();
            }
            return View(exams);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Faculty")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamName,Date,CourseId")] Exams exams)
        {
            if (id != exams.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamsExists(exams.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exams);
        }

        // GET: Exams/Delete/5
        [Authorize(Roles = "Admin, Faculty")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exams = await _context.Exams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exams == null)
            {
                return NotFound();
            }

            return View(exams);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Faculty")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exams = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamsExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
