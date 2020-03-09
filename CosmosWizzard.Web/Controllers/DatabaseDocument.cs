//namespace CosmosWizard.Web.Controllers
//{
//    using System.Linq;
//    using System.Threading.Tasks;
//    using CustomLogic.Database;
//    using Microsoft.AspNetCore.Mvc;
//    using Microsoft.EntityFrameworkCore;

//    public class DatabaseDocumentController : Controller
//    {
//        private readonly DatabaseMigratorContext _context;

//        public DatabaseDocumentController(DatabaseMigratorContext context)
//        {
//            _context = context;
//        }

//        // GET: DocumentRecords
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.DocumentRecords.ToListAsync());
//        }

//        // GET: DocumentRecords/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var DocumentRecords = await _context.DocumentRecords
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (DocumentRecords == null)
//            {
//                return NotFound();
//            }

//            return View(DocumentRecords);
//        }

//        // GET: DocumentRecords/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: DocumentRecords/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("CsFiles,TsFile,Id")] DocumentRecords DocumentRecords)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(DocumentRecords);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(DocumentRecords);
//        }

//        // GET: DocumentRecords/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var DocumentRecords = await _context.DocumentRecords.FindAsync(id);
//            if (DocumentRecords == null)
//            {
//                return NotFound();
//            }
//            return View(DocumentRecords);
//        }

//        // POST: DocumentRecords/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("CsFiles,TsFile,Id")] DocumentRecords DocumentRecords)
//        {
//            if (id != DocumentRecords.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(DocumentRecords);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!DocumentRecordsExists(DocumentRecords.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(DocumentRecords);
//        }

//        // GET: DocumentRecords/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var DocumentRecords = await _context.DocumentRecords
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (DocumentRecords == null)
//            {
//                return NotFound();
//            }

//            return View(DocumentRecords);
//        }

//        // POST: DocumentRecords/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var DocumentRecords = await _context.DocumentRecords.FindAsync(id);
//            _context.DocumentRecords.Remove(DocumentRecords);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool DocumentRecordsExists(int id)
//        {
//            return _context.DocumentRecords.Any(e => e.Id == id);
//        }
//    }
//}
