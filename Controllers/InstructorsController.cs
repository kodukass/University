using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using University.Data;
using University.Models;
using University.Models.SchoolViewModels;

namespace University.Controllers
{
	public class InstructorsController : Controller
	{
		private readonly SchoolContext _context;

		public InstructorsController
			(
				SchoolContext context
			)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(int? id, int? courseId)
		{
			var vm = new InstructorIndexData();

			vm.Instructors = await _context.Instructors
				.Include(i => i.OfficeAssignment)
				.Include(i => i.CourseAssignments)
				.ThenInclude(i => i.Course)
				.ThenInclude(i => i.Enrollments)
				.ThenInclude(i => i.Student)
				.Include(i => i.CourseAssignments)
				.ThenInclude(i => i.Course)
				.ThenInclude(i => i.Department)
				.AsNoTracking()
				.OrderBy(i => i.LastName)
				.ToListAsync();

			if(id != null)
			{
				ViewData["InstructorID"] = id.Value;
				Instructor instructor = vm.Instructors
					.Where(i => i.InstructorId == id.Value)
					.Single();
				vm.Courses = instructor.CourseAssignments
					.Select(i => i.Course);
			}

			if (courseId != null)
			{
				ViewData["CourseID"] = courseId.Value;
				vm.Enrollments = vm.Courses
					.Where(x => x.CourseID == courseId)
					.Single()
					.Enrollments;
			}

			return View(vm);
			
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var instructor = await _context.Instructors
				.FirstOrDefaultAsync(m => m.InstructorId == id);

			if (instructor == null)
			{
				return NotFound();
			}
			return View(instructor);
		}
	}
}
