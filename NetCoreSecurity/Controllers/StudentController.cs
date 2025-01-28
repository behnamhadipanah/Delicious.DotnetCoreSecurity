using Microsoft.AspNetCore.Mvc;
using NetCoreSecurity.Models.DataServices;
using NetCoreSecurity.Models.Student;

namespace NetCoreSecurity.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDataContext _context;

        public StudentController(StudentDataContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new List<CourseGrade>());
        }
        [HttpGet]
        public IActionResult AddGrade()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGrade(CourseGrade model)
        {
            if (!ModelState.IsValid)
                return View();

            model.CreatedDate = DateTime.Now;

            _context.Grades.Add(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(StudentController.Index), "Student");
        }
        [HttpGet]
        public IActionResult Classifications()
        {
            var classifications = new List<string>()
            {
                "Freshman",
                "Sophomore",
                "Junior",
                "Senior"
            };

            return View(classifications);
        }
    }
}
