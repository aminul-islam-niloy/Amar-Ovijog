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
    public class UnionsController : Controller
    {
        private readonly BdGeoServiceContext _context;

        public UnionsController(BdGeoServiceContext context)
        {
            _context = context;
        }

        // GET: Admin/Unions
        public async Task<IActionResult> Index()
        {
            var bdGeoServiceContext = _context.Unions.Include(u => u.Upazilla);
            return View(await bdGeoServiceContext.ToListAsync());
        }

        // GET: Admin/Unions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Unions == null)
            {
                return NotFound();
            }

            var union = await _context.Unions
                .Include(u => u.Upazilla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (union == null)
            {
                return NotFound();
            }

            return View(union);
        }

        // GET: Admin/Unions/Create
        public IActionResult Create()
        {
            ViewData["UpazillaId"] = new SelectList(_context.Upazilas, "Id", "Id");
            return View();
        }

        // POST: Admin/Unions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UpazillaId,Name,BnName,Url")] Union union)
        {
            if (ModelState.IsValid)
            {
                _context.Add(union);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UpazillaId"] = new SelectList(_context.Upazilas, "Id", "Id", union.UpazillaId);
            return View(union);
        }

        // GET: Admin/Unions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Unions == null)
            {
                return NotFound();
            }

            var union = await _context.Unions.FindAsync(id);
            if (union == null)
            {
                return NotFound();
            }
            ViewData["UpazillaId"] = new SelectList(_context.Upazilas, "Id", "Id", union.UpazillaId);
            return View(union);
        }

        // POST: Admin/Unions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UpazillaId,Name,BnName,Url")] Union union)
        {
            if (id != union.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(union);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnionExists(union.Id))
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
            ViewData["UpazillaId"] = new SelectList(_context.Upazilas, "Id", "Id", union.UpazillaId);
            return View(union);
        }

        // GET: Admin/Unions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Unions == null)
            {
                return NotFound();
            }

            var union = await _context.Unions
                .Include(u => u.Upazilla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (union == null)
            {
                return NotFound();
            }

            return View(union);
        }

        // POST: Admin/Unions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Unions == null)
            {
                return Problem("Entity set 'BdGeoServiceContext.Unions'  is null.");
            }
            var union = await _context.Unions.FindAsync(id);
            if (union != null)
            {
                _context.Unions.Remove(union);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnionExists(int id)
        {
          return (_context.Unions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
