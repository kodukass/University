using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using University.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _context.Students.ToListAsync();

            return View(result);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.
                Include(s => s.Enrollments).
                ThenInclude(e => e.Course).
                AsNoTracking().
                FirstOrDefaultAsync(m => m.StudentID == Id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
