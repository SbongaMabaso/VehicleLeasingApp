using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleLeasingApp.Data;
using VehicleLeasingApp.Models;

namespace VehicleLeasingApp.Controllers
{
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branches.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "No Branch Found";
                return NotFound();
            }

            var branches = await _context.Branches
                .FirstOrDefaultAsync(m => m.BranchId == id);
            if (branches == null)
            {
                TempData["Error"] = "Branch Not Found";
                return NotFound();
            }

            return View(branches);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchId,Name,Location")] Branches branches)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branches);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Branch Created Successfuly";
                return RedirectToAction(nameof(Index));
            }
            return View(branches);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.Branches.FindAsync(id);
            if (branches == null)
            {
                TempData["Error"] = "Branch Not Found";
                return NotFound();
            }
            return View(branches);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BranchId,Name,Location")] Branches branches)
        {
            if (id != branches.BranchId)
            {
                TempData["Error"] = "BranchID Not Found";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branches);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Branch Updated Successfuly";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchesExists(branches.BranchId))
                    {
                        TempData["Error"] = "Branch Not Found";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branches);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "BranchID Not Found";
                return NotFound();
            }

            var branches = await _context.Branches
                .FirstOrDefaultAsync(m => m.BranchId == id);
            if (branches == null)
            {
                TempData["Error"] = "Branch Not Found";
                return NotFound();
            }

            return View(branches);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branches = await _context.Branches.FindAsync(id);
            if (branches != null)
            {
                _context.Branches.Remove(branches);
                TempData["Warning"] = "Branch Deleted Successfuly";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchesExists(int id)
        {
            return _context.Branches.Any(e => e.BranchId == id);
        }
    }
}
