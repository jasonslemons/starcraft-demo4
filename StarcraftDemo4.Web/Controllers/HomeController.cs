using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarcraftDemo4.Data;
using StarcraftDemo4.Models;
using StarcraftDemo4.Web.Models;
using System.Diagnostics;

namespace StarcraftDemo4.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly StarcraftDbContext _context;

        public HomeController(StarcraftDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recentGames = await _context.Games
                .OrderByDescending(g => g.StartTime)
                .Take(10)
                .ToListAsync();

            return View(recentGames);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}