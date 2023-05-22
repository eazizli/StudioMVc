using Microsoft.AspNetCore.Mvc;
using StudioaMvc.DataContext;

namespace StudioaMvc.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class DashBoardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
