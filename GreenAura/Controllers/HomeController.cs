using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GreenAura.Models;
using GreenAura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GreenAura.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewStates()
        {
            return View(await _context.States.ToListAsync());
        }

        // GET: Locations According to State
        public async Task<IActionResult> ViewStateLocation(int? id)
        {
            var applicationDbContext = _context.Locations
                .Include(b => b.State).Where(m => m.StateID == id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ViewAllLocation()
        {
            return View(await _context.Locations.ToListAsync());
        }

        public async Task<IActionResult> ViewTypes()
        {
            return View(await _context.PlantTypes.ToListAsync());
        }

        public async Task<IActionResult> ViewAllProducts()
        {
            var applicationDbContext = _context.ProductInfos
                .Include(b => b.PlantType);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> ViewProductByType(int? id)
        {
            var applicationDbContext = _context.ProductInfos
                .Include(b => b.PlantType).Where(m => m.PlantTypeID == id);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.ProductInfos
                .Include(b => b.PlantType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
