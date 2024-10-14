using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmarOvijog.Models;
using X.PagedList;
using X.PagedList.Extensions;


namespace AmarOvijog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DistrictsController : Controller
    {
        private readonly BdGeoServiceContext _context;

        public DistrictsController(BdGeoServiceContext context)
        {
            _context = context;
        }

        // GET: Admin/Districts
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 20;  
            int pageNumber = page ?? 1;
            var bdGeoServiceContext = _context.Districts.Include(d => d.Division);
            var data = await bdGeoServiceContext.ToListAsync();
            var paginatedData = data.ToPagedList(pageNumber, pageSize);
            return View(paginatedData);
        }

        // GET: Admin/Districts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Districts == null)
            {
                return NotFound();
            }

            var district = await _context.Districts
                .Include(d => d.Division)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // GET: Admin/Districts/Create
        public IActionResult Create()
        {
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id");
            return View();
        }

        // POST: Admin/Districts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DivisionId,Name,BnName,Lat,Lon,Url")] District district)
        {
            if (ModelState.IsValid)
            {
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", district.DivisionId);
            return View(district);
        }

        // GET: Admin/Districts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Districts == null)
            {
                return NotFound();
            }

            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", district.DivisionId);
            return View(district);
        }

        // POST: Admin/Districts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DivisionId,Name,BnName,Lat,Lon,Url")] District district)
        {
            if (id != district.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictExists(district.Id))
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
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Id", district.DivisionId);
            return View(district);
        }

        // GET: Admin/Districts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Districts == null)
            {
                return NotFound();
            }

            var district = await _context.Districts
                .Include(d => d.Division)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // POST: Admin/Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Districts == null)
            {
                return Problem("Entity set 'BdGeoServiceContext.Districts'  is null.");
            }
            var district = await _context.Districts.FindAsync(id);
            if (district != null)
            {
                _context.Districts.Remove(district);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictExists(int id)
        {
          return (_context.Districts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
