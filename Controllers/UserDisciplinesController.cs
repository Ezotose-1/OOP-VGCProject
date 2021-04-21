using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOP_VGCProject.Data;
using OOP_VGCProject.Models;

namespace OOP_VGCProject.Controllers
{
    public class UserDisciplinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDisciplinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserDisciplines
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserDiscipline.ToListAsync());
        }

        // GET: UserDisciplines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserDisciplines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,DisciplineId")] UserDiscipline userDiscipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDiscipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDiscipline);
        }

        // GET: UserDisciplines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDiscipline = await _context.UserDiscipline.FindAsync(id);
            if (userDiscipline == null)
            {
                return NotFound();
            }
            return View(userDiscipline);
        }

        // POST: UserDisciplines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DisciplineId")] UserDiscipline userDiscipline)
        {
            if (id != userDiscipline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDiscipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDisciplineExists(userDiscipline.Id))
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
            return View(userDiscipline);
        }

        // GET: UserDisciplines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDiscipline = await _context.UserDiscipline
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userDiscipline == null)
            {
                return NotFound();
            }

            return View(userDiscipline);
        }

        // POST: UserDisciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userDiscipline = await _context.UserDiscipline.FindAsync(id);
            _context.UserDiscipline.Remove(userDiscipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDisciplineExists(int id)
        {
            return _context.UserDiscipline.Any(e => e.Id == id);
        }
    }
}
