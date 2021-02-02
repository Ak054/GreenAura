using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenAura.Data;
using GreenAura.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenAura.Controllers
{
    [Authorize(Roles = "administrator")]
    public class LocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Locations.Include(c => c.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Location = await _context.Locations
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (Location == null)
            {
                return NotFound();
            }

            return View(Location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["StateID"] = new SelectList(_context.States, "StateID", "StateName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,LocationName,AddressLine1,AddressLine2,ContactNo,StateID")] Location Location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateID"] = new SelectList(_context.States, "StateID", "StateName", Location.StateID);
            return View(Location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Location = await _context.Locations.FindAsync(id);
            if (Location == null)
            {
                return NotFound();
            }
            ViewData["StateID"] = new SelectList(_context.States, "StateID", "StateName", Location.StateID);
            return View(Location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,LocationName,AddressLine1,AddressLine2,ContactNo,StateID")] Location Location)
        {
            if (id != Location.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(Location.LocationID))
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
            ViewData["StateID"] = new SelectList(_context.States, "StateID", "StateName", Location.StateID);
            return View(Location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Location = await _context.Locations
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (Location == null)
            {
                return NotFound();
            }

            return View(Location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(Location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.LocationID == id);
        }
    }
}
