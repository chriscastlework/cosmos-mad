namespace CosmosWizard.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CustomLogic.Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class CreatedFilesController : Controller
    {
        private readonly DatabaseMigratorContext _context;

        public CreatedFilesController(DatabaseMigratorContext context)
        {
            _context = context;
        }

        // GET: CreatedFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.CreatedFiles.ToListAsync());
        }

        // GET: CreatedFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CreatedFiles = await _context.CreatedFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CreatedFiles == null)
            {
                return NotFound();
            }

            return View(CreatedFiles);
        }

        // GET: CreatedFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreatedFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CsFiles,TsFile,Id")] CreatedFiles CreatedFiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CreatedFiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CreatedFiles);
        }

        // GET: CreatedFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CreatedFiles = await _context.CreatedFiles.FindAsync(id);
            if (CreatedFiles == null)
            {
                return NotFound();
            }
            return View(CreatedFiles);
        }

        // POST: CreatedFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CsFiles,TsFile,Id")] CreatedFiles CreatedFiles)
        {
            if (id != CreatedFiles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CreatedFiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreatedFilesExists(CreatedFiles.Id))
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
            return View(CreatedFiles);
        }

        // GET: CreatedFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CreatedFiles = await _context.CreatedFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CreatedFiles == null)
            {
                return NotFound();
            }

            return View(CreatedFiles);
        }

        // POST: CreatedFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CreatedFiles = await _context.CreatedFiles.FindAsync(id);
            _context.CreatedFiles.Remove(CreatedFiles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreatedFilesExists(int id)
        {
            return _context.CreatedFiles.Any(e => e.Id == id);
        }
    }
}
