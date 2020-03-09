namespace CosmosWizard.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using CustomLogic.Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseDocumentController : Controller
    {
        private readonly DatabaseMigratorContext _context;

        public DatabaseDocumentController(DatabaseMigratorContext context)
        {
            _context = context;
        }

        // GET: DocumentRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentRecords.Where(c=>c.EntityType == "Action").ToListAsync());
        }

        // GET: DocumentRecords/Details/5
        public async Task<IActionResult> Details(string id, string partition)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DocumentRecords = await _context.DocumentRecords
                .FirstOrDefaultAsync(m => m.Id == id && m.Partition == partition);
            if (DocumentRecords == null)
            {
                return NotFound();
            }

            return View(DocumentRecords);
        }

        // GET: DocumentRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: DocumentRecords/Edit/5
        public async Task<IActionResult> Edit(string id,  string partition)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DocumentRecords = await _context.DocumentRecords.FindAsync(id, partition);
            if (DocumentRecords == null)
            {
                return NotFound();
            }
            return View(DocumentRecords);
        }
    }
}
