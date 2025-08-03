using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VehicleLeasingApp.Data;
using VehicleLeasingApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehicleLeasingApp.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Manufacturer).Include(v => v.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Manufacturer)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "CompanyName");
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FullName");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,RegistrationNumber,Model,ManufacturerId,SupplierId,BranchId,ClientId,DriverId,LeaseStartDate,LeaseEndDate")] Vehicles vehicles)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(vehicles);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Vehicle added successfully!";
                return RedirectToAction(nameof(Index));
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            //Console.WriteLine($"Errors: {errors}");
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "Name", vehicles.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "CompanyName", vehicles.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FullName", vehicles.DriverId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", vehicles.ManufacturerId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", vehicles.SupplierId);
            
            TempData["Error"] = "Failed to add vehicle.";
            return View(vehicles);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Selected VehicleID Not Found";
                return NotFound();
            }

            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles == null)
            {
                TempData["Error"] = "Selected Vehicle Not Found";
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "Name", vehicles.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "CompanyName", vehicles.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FullName", vehicles.DriverId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", vehicles.ManufacturerId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", vehicles.SupplierId);
            
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,RegistrationNumber,Model,ManufacturerId,SupplierId,BranchId,ClientId,DriverId,LeaseStartDate,LeaseEndDate")] Vehicles vehicles)
        {
            if (id != vehicles.VehicleId)
            {
                TempData["Error"] = "Selected VehicleID Not Found";
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicles);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Vehicle updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesExists(vehicles.VehicleId))
                    {
                        TempData["Error"] = "Selected VehicleID Not Found";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "BranchId", "Name", vehicles.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "CompanyName", vehicles.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Drivers, "DriverId", "FullName", vehicles.DriverId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "Name", vehicles.ManufacturerId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Name", vehicles.SupplierId);
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Selected VehicleID Not Found";
                return NotFound();
            }

            var vehicles = await _context.Vehicles
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Manufacturer)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicles == null)
            {
                TempData["Error"] = "Selected Vehicle Not Found";
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicles = await _context.Vehicles.FindAsync(id);
            if (vehicles != null)
            {
                _context.Vehicles.Remove(vehicles);
                TempData["Success"] = "Vehicle Deleted Successfully!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
