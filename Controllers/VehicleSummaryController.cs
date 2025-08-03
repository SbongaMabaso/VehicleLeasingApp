using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleLeasingApp.Data;
using VehicleLeasingApp.Models;

namespace VehicleLeasingApp.Controllers
{
    public class VehicleSummaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleSummaryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var supplierSummary = await _context.Vehicles
                .Where(v => v.Supplier != null && v.Manufacturer != null)
                .GroupBy(v => new { SupplierName = v.Supplier.Name, ManufacturerName = v.Manufacturer.Name })
                .Select(g => new SupplierSummary
                {
                    Supplier = g.Key.SupplierName,
                    Manufacturer = g.Key.ManufacturerName,
                    Count = g.Count()
                }).ToListAsync();

            var branchSummary = await _context.Vehicles
                .Where(v => v.Branch != null)
                .GroupBy(v => v.Branch.Name)
                .Select(g => new BranchSummary
                {
                    Branch = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var clientSummary = await _context.Vehicles
                .Where(v => v.Client != null)
                .GroupBy(v => v.Client.CompanyName)
                .Select(g => new ClientSummary
                {
                    Client = g.Key,
                    Count = g.Count()
                }).ToListAsync();

            var model = new VehicleSummaryViewModel
            {
                supplierSummaries = supplierSummary,
                branchSummaries = branchSummary,
                clientSummaries = clientSummary
            };

            return View(model);
        }
    }
}
