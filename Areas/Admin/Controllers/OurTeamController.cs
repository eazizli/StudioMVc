using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic;
using StudioaMvc.DataContext;
using StudioaMvc.Models;

namespace StudioaMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurTeamController : Controller
    {
        private readonly StudioDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OurTeamController(StudioDbContext studioDbContext,IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = studioDbContext;
        }

        public  IActionResult Index()
        {
            List<OurTeam> teams=_dbContext.OurTeams.ToList();   
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OurTeam ourTeam)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            string guid = Guid.NewGuid().ToString();
            string newfile = guid + ourTeam.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", newfile);
            using (FileStream fs = new FileStream(path, FileMode.CreateNew))
            {
                await ourTeam.Images.CopyToAsync(fs);
            }
            OurTeam our = new OurTeam();
            {
                our.ImageName = newfile;
                our.Name = ourTeam.Name;
                our.Work=ourTeam.Work;
            }
            await _dbContext.AddAsync(our);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            OurTeam ourteam = await _dbContext.OurTeams.FindAsync(id);
            if (ourteam == null)
            {
                return NotFound();
            }

            return View(ourteam);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,OurTeam newteam)
        {
                OurTeam teams=await _dbContext.OurTeams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }    
            teams.Name = newteam.Name;
            teams.Work = newteam.Work;
            string guid=Guid.NewGuid().ToString();
            string newfile = guid + newteam.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", newfile);
            using(FileStream filestrem=new FileStream(path, FileMode.Create))
            {
                await newteam.Images.CopyToAsync(filestrem);
            }
            teams.ImageName = newfile;
            teams.Name=newteam.Name;
            teams.Work=newteam.Work;
            _dbContext.OurTeams.Update(teams);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            OurTeam team=await _dbContext.OurTeams.FindAsync(id);
            if(team == null)
            {
                return View();
            }
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _dbContext.OurTeams.Remove(team);

            await _dbContext.SaveChangesAsync();  
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Detail(int id)
        {
            OurTeam team=await _dbContext.OurTeams.FindAsync(id);
            return View(team);

        }
    }
}
