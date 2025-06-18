using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarcraftDemo4.Data;
using StarcraftDemo4.Models;

namespace StarcraftDemo4.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly StarcraftDbContext _context;

        public GamesController(StarcraftDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, DateTime? startDate, DateTime? endDate, int? minDuration, int? maxDuration)
        {
            var games = _context.Games.AsQueryable();

            if (startDate.HasValue)
            {
                games = games.Where(g => g.StartTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                games = games.Where(g => g.StartTime <= endDate.Value);
            }

            if (minDuration.HasValue)
            {
                games = games.Where(g => g.TotalGameTime >= minDuration.Value);
            }

            if (maxDuration.HasValue)
            {
                games = games.Where(g => g.TotalGameTime <= maxDuration.Value);
            }

            ViewBag.SearchTerm = searchTerm;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.MinDuration = minDuration;
            ViewBag.MaxDuration = maxDuration;

            var gamesList = await games
                .OrderByDescending(g => g.StartTime)
                .ToListAsync();

            return View(gamesList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var game = await _context.Games
                .Include(g => g.GameSteps)
                .FirstOrDefaultAsync(g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public async Task<IActionResult> Steps(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.GameSteps.OrderBy(s => s.StepNumber))
                .FirstOrDefaultAsync(g => g.GameId == gameId);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}