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
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public FeesController(ApplicationDbContext context,
                              RoleManager<IdentityRole> roleManager,
                              UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Fees
        public async Task<IActionResult> Index()
        {
//            int loggedUserId = int.Parse(User.Identity.Name);
//            var user = _userManager.FindByNameAsync(User.Identity.Name);

            string loggedUserId = _userManager.GetUserId(User);
            var FeesList = await _context.Fees.ToListAsync();
            var PaidFeeID = await _context.UserFee.Where(x => x.PaidUserId == loggedUserId).ToListAsync();
            List<UserFeeDTO> DTOLst = new List<UserFeeDTO>();

            foreach (var fee in FeesList)
            {
                var DTO = new UserFeeDTO
                {
                    Fee = fee,
                    IsPaid = PaidFeeID.Exists(f => f.FeeId == fee.Id),
                };
                DTOLst.Add(DTO);
            }

            return View(DTOLst);
//            return View(await _context.Fees.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int id)
        {
            string loggedUserId = _userManager.GetUserId(User);
            var Fee = await _context.Fees.FindAsync(id);
            var userFee = new UserFee
            {
                PaidUserId = loggedUserId,
                FeeId = Fee.Id
            };
            _context.UserFee.Add(userFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }







        // GET: Fees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fees == null)
            {
                return NotFound();
            }

            return View(fees);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Name")] Fees fees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fees);
        }

        // GET: Fees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees.FindAsync(id);
            if (fees == null)
            {
                return NotFound();
            }
            return View(fees);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Name")] Fees fees)
        {
            if (id != fees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeesExists(fees.Id))
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
            return View(fees);
        }

        // GET: Fees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fees == null)
            {
                return NotFound();
            }

            return View(fees);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fees = await _context.Fees.FindAsync(id);
            _context.Fees.Remove(fees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeesExists(int id)
        {
            return _context.Fees.Any(e => e.Id == id);
        }
    }
}
