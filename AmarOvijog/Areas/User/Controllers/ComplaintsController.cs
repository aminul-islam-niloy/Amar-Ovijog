using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmarOvijog.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;

namespace AmarOvijog.Areas.User.Controllers
{
    [Area("User")]
    public class ComplaintsController : Controller
    {
        private readonly BdGeoServiceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ComplaintsController(BdGeoServiceContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: User/Complaints
        public async Task<IActionResult> Index()
        {
            var bdGeoServiceContext = _context.Complaints.Include(c => c.District).Include(c => c.Division).Include(c => c.Union).Include(c => c.Upazila).Include(c => c.User);
            return View(await bdGeoServiceContext.ToListAsync());
        }

        // GET: User/Complaints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.District)
                .Include(c => c.Division)
                .Include(c => c.Union)
                .Include(c => c.Upazila)
                .Include(c => c.User)
                .Include(c => c.ComplaintImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            ViewBag.DivisionName = _context.Divisions.FirstOrDefault(d => d.Id == complaint.DivisionId)?.Name;
            ViewBag.DistrictName = _context.Districts.FirstOrDefault(d => d.Id == complaint.DistrictId)?.Name;
            ViewBag.UpazilaName = _context.Upazilas.FirstOrDefault(u => u.Id == complaint.UpazilaId)?.Name;
            ViewBag.UnionName = _context.Unions.FirstOrDefault(u => u.Id == complaint.UnionId)?.Name;

            return View(complaint);
        }

        // GET: User/Complaints/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Name");
            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Complaint complaint, List<IFormFile> images)
        {
            // Automatically set the current user's ID
            complaint.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
                _context.Add(complaint);
                await _context.SaveChangesAsync();

            
            // Handle image uploads
            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                    // Save the image to the server
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Create a new ComplaintImage entity
                    var complaintImage = new ComplaintImage
                    {
                        ComplaintId = complaint.Id,
                        ImageUrl = "/Images/" + fileName 
                    };

                    // Add the image to the context
                    _context.ComplaintImages.Add(complaintImage);
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    

        // AJAX Call to get districts based on selected division
        public JsonResult GetDistricts(int divisionId)
        {
            var districts = _context.Districts
                .Where(d => d.DivisionId == divisionId)
                .Select(d => new { d.Id, d.Name }).ToList();
            return Json(districts);
        }

        // AJAX Call to get upazilas based on selected district
        public JsonResult GetUpazilas(int districtId)
        {
            var upazilas = _context.Upazilas
                .Where(u => u.DistrictId == districtId)
                .Select(u => new { u.Id, u.Name }).ToList();
            return Json(upazilas);
        }

        // AJAX Call to get unions based on selected upazila
        public JsonResult GetUnions(int upazilaId)
        {
            var unions = _context.Unions
                .Where(u => u.UpazillaId == upazilaId)
                .Select(u => new { u.Id, u.Name }).ToList();
            return Json(unions);
        }
        // GET: User/Complaints/Edit/5

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.District)
                .Include(c => c.Division)
                .Include(c => c.Union)
                .Include(c => c.Upazila)
                .Include(c => c.User)
                .Include(c => c.ComplaintImages) // Load existing images
                .FirstOrDefaultAsync(m => m.Id == id);

            if (complaint == null)
            {
                return NotFound();
            }

            ViewData["DivisionId"] = new SelectList(_context.Divisions, "Id", "Name", complaint.DivisionId);
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "Name", complaint.DistrictId);
            ViewData["UpazilaId"] = new SelectList(_context.Upazilas, "Id", "Name", complaint.UpazilaId);
            ViewData["UnionId"] = new SelectList(_context.Unions, "Id", "Name", complaint.UnionId);

            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Complaint complaint, List<IFormFile> images)
        {
            // Automatically set the current user's ID
            complaint.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Handle image uploads
            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                    // Save the image to the server
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Create a new ComplaintImage entity
                    var complaintImage = new ComplaintImage
                    {
                        ComplaintId = complaint.Id,
                        ImageUrl = "/Images/" + fileName
                    };

                    // Add the image to the context
                    _context.ComplaintImages.Update(complaintImage);
                }
            }

            _context.Update(complaint);
            await _context.SaveChangesAsync();
      

            return RedirectToAction(nameof(Index));
        }


        // GET: User/Complaints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.District)
                .Include(c => c.Division)
                .Include(c => c.Union)
                .Include(c => c.Upazila)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: User/Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'BdGeoServiceContext.Complaints'  is null.");
            }
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(int id)
        {
          return (_context.Complaints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
