using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;

namespace UI.Controllers
{
    public class ContractorsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContractorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string? searchName, string? sortField, string? sortDir, int page = 1, int pageSize = 10)
        {
            var query = _db.Contractors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(c => c.Name.Contains(searchName));

            // Sorting
            sortField = sortField ?? "Name";
            sortDir = sortDir ?? "asc";

            if (sortField == "Name")
                query = sortDir == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
            else if (sortField == "Status")
                query = sortDir == "asc" ? query.OrderBy(c => c.IsActive) : query.OrderByDescending(c => c.IsActive);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["SearchName"] = searchName;
            ViewData["SortField"] = sortField;
            ViewData["SortDir"] = sortDir;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["Total"] = total;

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Grid(string? searchName, string? sortField, string? sortDir, int page = 1, int pageSize = 10)
        {
            var query = _db.Contractors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchName))
                query = query.Where(c => c.Name.Contains(searchName));

            sortField = sortField ?? "Name";
            sortDir = sortDir ?? "asc";

            if (sortField == "Name")
                query = sortDir == "asc" ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
            else if (sortField == "Status")
                query = sortDir == "asc" ? query.OrderBy(c => c.IsActive) : query.OrderByDescending(c => c.IsActive);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["Total"] = total;

            return PartialView("_ContractorGrid", items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Contractor model)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // return validation errors
                    var errors = ModelState.Where(kvp => kvp.Value.Errors.Any())
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                    return BadRequest(new { success = false, errors });
                }

                return RedirectToAction(nameof(Index));
            }

            _db.Contractors.Add(model);
            await _db.SaveChangesAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _db.Contractors.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contractor model)
        {
            if (id != model.Id) return BadRequest();
            var item = await _db.Contractors.FindAsync(id);
            if (item == null) return NotFound();
            item.Name = model.Name;
            item.Address = model.Address;
            item.Phone = model.Phone;
            item.IsActive = model.IsActive;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Contractors.FindAsync(id);
            if (item == null) return NotFound();
            _db.Contractors.Remove(item);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
