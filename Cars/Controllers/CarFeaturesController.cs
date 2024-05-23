using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cars.DataBase;
using Cars.Models;

namespace Cars.Controllers
{
    public class CarFeaturesController : Controller
    {
        private readonly DB _context;

        public CarFeaturesController(DB context)
        {
            _context = context;
        }

        // GET: CarFeatures
        public async Task<IActionResult> Index()
        {
            var dB = _context.CarFeatures.Include(c => c.Car).Include(c => c.Feature);
            return View(await dB.ToListAsync());
        }

        // GET: CarFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarFeatures == null)
            {
                return NotFound();
            }

            var carFeature = await _context.CarFeatures
                .Include(c => c.Car)
                .Include(c => c.Feature)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carFeature == null)
            {
                return NotFound();
            }

            return View(carFeature);
        }

        // GET: CarFeatures/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Id");
            return View();
        }

        // POST: CarFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,FeatureId")] CarFeature carFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carFeature.CarId);
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Id", carFeature.FeatureId);
            return View(carFeature);
        }

        // GET: CarFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarFeatures == null)
            {
                return NotFound();
            }

            var carFeature = await _context.CarFeatures.FindAsync(id);
            if (carFeature == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carFeature.CarId);
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Id", carFeature.FeatureId);
            return View(carFeature);
        }

        // POST: CarFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,FeatureId")] CarFeature carFeature)
        {
            if (id != carFeature.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarFeatureExists(carFeature.CarId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carFeature.CarId);
            ViewData["FeatureId"] = new SelectList(_context.Features, "Id", "Id", carFeature.FeatureId);
            return View(carFeature);
        }

        // GET: CarFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarFeatures == null)
            {
                return NotFound();
            }

            var carFeature = await _context.CarFeatures
                .Include(c => c.Car)
                .Include(c => c.Feature)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (carFeature == null)
            {
                return NotFound();
            }

            return View(carFeature);
        }

        // POST: CarFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarFeatures == null)
            {
                return Problem("Entity set 'DB.CarFeatures'  is null.");
            }
            var carFeature = await _context.CarFeatures.FindAsync(id);
            if (carFeature != null)
            {
                _context.CarFeatures.Remove(carFeature);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarFeatureExists(int id)
        {
          return (_context.CarFeatures?.Any(e => e.CarId == id)).GetValueOrDefault();
        }
    }
}
