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
    public class ManufacturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManufacturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manufacturers.ToListAsync());
        }

        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturers == null)
            {
                TempData["Error"] = "Manufacturer Detials Not Found";
                return NotFound();
            }

            return View(manufacturers);
        }

        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManufacturerId,Name")] Manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manufacturers);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Created Successfuly";
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturers);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.Manufacturers.FindAsync(id);
            if (manufacturers == null)
            {
                TempData["Error"] = "Detials Not Found";
                return NotFound();
            }
            return View(manufacturers);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManufacturerId,Name")] Manufacturers manufacturers)
        {
            if (id != manufacturers.ManufacturerId)
            {
                TempData["Error"] = "Invalid ID";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manufacturers);
                    TempData["Success"] = "Updated Successfuly";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturersExists(manufacturers.ManufacturerId))
                    {
                        TempData["Error"] = "Detials Not Found";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(manufacturers);
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturers = await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturers == null)
            {
                TempData["Error"] = "Detials Not Found";
                return NotFound();
            }

            return View(manufacturers);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturers = await _context.Manufacturers.FindAsync(id);
            if (manufacturers != null)
            {
                _context.Manufacturers.Remove(manufacturers);
                TempData["Success"] = "Deleted Successfuly";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturersExists(int id)
        {
            return _context.Manufacturers.Any(e => e.ManufacturerId == id);
        }
    }
}
