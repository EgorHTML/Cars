﻿using System;
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
    public class BodyTypesController : Controller
    {
        private readonly DB _context;

        public BodyTypesController(DB context)
        {
            _context = context;
        }

        // GET: BodyTypes
        public async Task<IActionResult> Index()
        {
              return _context.BodyTypes != null ? 
                          View(await _context.BodyTypes.ToListAsync()) :
                          Problem("Entity set 'DB.BodyTypes'  is null.");
        }

        // GET: BodyTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BodyTypes == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }

        // GET: BodyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BodyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BodyType bodyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyType);
        }

        // GET: BodyTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BodyTypes == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes.FindAsync(id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }

        // POST: BodyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BodyType bodyType)
        {
            if (id != bodyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyTypeExists(bodyType.Id))
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
            return View(bodyType);
        }

        // GET: BodyTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BodyTypes == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }

        // POST: BodyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BodyTypes == null)
            {
                return Problem("Entity set 'DB.BodyTypes'  is null.");
            }
            var bodyType = await _context.BodyTypes.FindAsync(id);
            if (bodyType != null)
            {
                _context.BodyTypes.Remove(bodyType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyTypeExists(int id)
        {
          return (_context.BodyTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
