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
    public class ProductInfosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductInfosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductInfos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductInfos.Include(e => e.PlantType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductInfos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProductInfo = await _context.ProductInfos
                .Include(e => e.PlantType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (ProductInfo == null)
            {
                return NotFound();
            }

            return View(ProductInfo);
        }

        // GET: ProductInfos/Create
        public IActionResult Create()
        {
            ViewData["PlantTypeID"] = new SelectList(_context.PlantTypes, "PlantTypeID", "PlantTypeName");
            return View();
        }

        // POST: ProductInfos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Details,Price,Quantity,PlantTypeID")] ProductInfo ProductInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ProductInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantTypeID"] = new SelectList(_context.PlantTypes, "PlantTypeID", "PlantTypeName", ProductInfo.PlantTypeID);
            return View(ProductInfo);
        }

        // GET: ProductInfos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProductInfo = await _context.ProductInfos.FindAsync(id);
            if (ProductInfo == null)
            {
                return NotFound();
            }
            ViewData["PlantTypeID"] = new SelectList(_context.PlantTypes, "PlantTypeID", "PlantTypeName", ProductInfo.PlantTypeID);
            return View(ProductInfo);
        }

        // POST: ProductInfos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,Details,Price,Quantity,PlantTypeID")] ProductInfo ProductInfo)
        {
            if (id != ProductInfo.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ProductInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInfoExists(ProductInfo.ProductID))
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
            ViewData["PlantTypeID"] = new SelectList(_context.PlantTypes, "PlantTypeID", "PlantTypeName", ProductInfo.PlantTypeID);
            return View(ProductInfo);
        }

        // GET: ProductInfos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProductInfo = await _context.ProductInfos
                .Include(e => e.PlantType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (ProductInfo == null)
            {
                return NotFound();
            }

            return View(ProductInfo);
        }

        // POST: ProductInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ProductInfo = await _context.ProductInfos.FindAsync(id);
            _context.ProductInfos.Remove(ProductInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInfoExists(int id)
        {
            return _context.ProductInfos.Any(e => e.ProductID == id);
        }
    }
}
