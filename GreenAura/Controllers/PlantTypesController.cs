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
    public class PlantTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlantTypes.ToListAsync());
        }

        // GET: PlantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PlantType = await _context.PlantTypes
                .FirstOrDefaultAsync(m => m.PlantTypeID == id);
            if (PlantType == null)
            {
                return NotFound();
            }

            return View(PlantType);
        }

        // GET: PlantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlantTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantTypeID,PlantTypeName")] PlantType PlantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PlantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PlantType);
        }

        // GET: PlantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PlantType = await _context.PlantTypes.FindAsync(id);
            if (PlantType == null)
            {
                return NotFound();
            }
            return View(PlantType);
        }

        // POST: PlantTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantTypeID,PlantTypeName")] PlantType PlantType)
        {
            if (id != PlantType.PlantTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PlantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantTypeExists(PlantType.PlantTypeID))
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
            return View(PlantType);
        }

        // GET: PlantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PlantType = await _context.PlantTypes
                .FirstOrDefaultAsync(m => m.PlantTypeID == id);
            if (PlantType == null)
            {
                return NotFound();
            }

            return View(PlantType);
        }

        // POST: PlantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PlantType = await _context.PlantTypes.FindAsync(id);
            _context.PlantTypes.Remove(PlantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantTypeExists(int id)
        {
            return _context.PlantTypes.Any(e => e.PlantTypeID == id);
        }
    }
}
