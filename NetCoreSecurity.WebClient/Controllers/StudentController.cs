﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCoreSecurity.WebClient.Models.DataServices;
using NetCoreSecurity.WebClient.Models.Student;

namespace NetCoreSecurity.WebClient.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]//xss valid
    //[EnableCors("MyCors")]
    [RequireHttps]
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
            string username = User.Identity.Name;
            IEnumerable<CourseGrade> grades = _context.Grades.Where(g => g.StudentUsername == username);
            return View(grades);
        }
        [HttpGet]
        [Authorize(Policy = "FacultyOnly")]
        public IActionResult AddGrade()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "FacultyOnly")]
        [ValidateAntiForgeryToken] //xss valid
        public IActionResult AddGrade(CourseGrade model)
        {
            if (!ModelState.IsValid)
                return View();

            model.CreatedDate = DateTime.Now;

            _context.Grades.Add(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), "Student");
        }

        [HttpGet]
        [AllowAnonymous]
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
