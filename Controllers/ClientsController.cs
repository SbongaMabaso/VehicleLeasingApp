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
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (clients == null)
            {
                TempData["Error"] = "Client Not Found";
                return NotFound();
            }

            return View(clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,CompanyName,ContactPerson,Phone")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clients);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Client Created Successfuly";
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Invalid Client ID";
                return NotFound();
            }

            var clients = await _context.Clients.FindAsync(id);
            if (clients == null)
            {
                TempData["Error"] = "Client Not Found";
                return NotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,CompanyName,ContactPerson,Phone")] Clients clients)
        {
            if (id != clients.ClientId)
            {
                TempData["Error"] = "Invalid Client ID";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clients);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Client Updates Successfuly";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.ClientId))
                    {
                        TempData["Error"] = "Invalid Client ID";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Invalid Client ID";
                return NotFound();
            }

            var clients = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (clients == null)
            {
                TempData["Error"] = "Client Not Found";
                return NotFound();
            }

            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clients = await _context.Clients.FindAsync(id);
            if (clients != null)
            {
                _context.Clients.Remove(clients);
                TempData["Success"] = "Client Deleted Successfuly";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
