using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmarOvijog.Models;

namespace AmarOvijog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DivisionsController : Controller
    {
        private readonly BdGeoServiceContext _context;

        public DivisionsController(BdGeoServiceContext context)
        {
            _context = context;
        }

        // GET: Admin/Divisions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Divisions.ToListAsync());
        }

        // GET: Admin/Divisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // GET: Admin/Divisions/Create
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BnName,Url")] Division division)
        {
            if (ModelState.IsValid)
            {
                _context.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(division);
        }

        // GET: Admin/Divisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }
            return View(division);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BnName,Url")] Division division)
        {
            if (id != division.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(division);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionExists(division.Id))
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
            return View(division);
        }

        // GET: Admin/Divisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Divisions == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // POST: Admin/Divisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Divisions == null)
            {
                return Problem("Entity set 'BdGeoServiceContext.Divisions'  is null.");
            }
            var division = await _context.Divisions.FindAsync(id);
            if (division != null)
            {
                _context.Divisions.Remove(division);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DivisionExists(int id)
        {
          return (_context.Divisions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
