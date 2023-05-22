using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioaMvc.DataContext;
using StudioaMvc.Models;

namespace StudioaMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudioDbContext _studioDb;

        public HomeController(StudioDbContext studioDb)
        {
            _studioDb = studioDb;
        }

        public async Task<IActionResult> Index()
        {
            List<OurTeam> ourTeams = await _studioDb.OurTeams.ToListAsync();
            return View(ourTeams);
        }
    }
}
