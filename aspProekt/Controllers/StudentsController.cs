using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspProekt.Model;
using aspProekt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace aspProekt.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AspProektContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment he;
        private readonly IWebHostEnvironment hostEnvironment;

        public StudentsController(AspProektContext context, IWebHostEnvironment e, IWebHostEnvironment hostEnvironment)
        {
            he = e;
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }
        //private readonly aspProektContext _context;

        //public StudentsController(aspProektContext context)
        //{
        //    _context = context;
        //}
        [HttpPost]
        public async Task<IActionResult> showPicture(string firstName, string lastName, IFormFile pic)
        {
            ViewData["fname"] = firstName;
            if (pic != null)
            {
                var filename = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(filename, FileMode.Create));
                ViewData["filelocation"] = "/" + Path.GetFileName(pic.FileName);
            }
            //napravi kopija od izbraniot student
            //var selected = await _context.Student.Where(s => s.FirstName.Equals(firstName) && s.LastName.Equals(lastName)).FirstOrDefaultAsync();
            var selected = await _context.Student.FirstOrDefaultAsync(s => s.FirstName.Equals(firstName) && s.LastName.Equals(lastName));
            selected.pic = "/" + Path.GetFileName(pic.FileName);

            //vnesi go vo databaza
            //_context.Add(selected);
            _context.Update(selected);
            await _context.SaveChangesAsync();
            var pom = from p in _context.Student
                      select p;
            pom = pom.Where(p => p.ID == selected.ID);
            return View();

        }
        public async Task<IActionResult> addPicture(int? id)
        {
            return View();
        }
        public async Task<IActionResult> searchByCourse(string searchCourse)
        {
            if (string.IsNullOrEmpty(searchCourse))
            {
                return View(await _context.Student.ToListAsync());
            }
            var courses = from a in _context.Course
                          select a;
            var students = from m in _context.Student
                           select m;
            int x = 0;

            Int32.TryParse(searchCourse, out x);

            courses = courses.Where(z => z.Title.Contains(searchCourse));

            students = students.Where(s => s.Courses.Any(m => m.ID == x));

            return View(await students.ToListAsync());
        }
        //// GET: Students
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Student.ToListAsync());
        //}
        public async Task<IActionResult> Index(string searchName, string searchLastName, string searchStudentID)
        {
            var students = from m in _context.Student
                           select m;

            if (!String.IsNullOrEmpty(searchName))
            {
                students = students.Where(s => s.FirstName.Contains(searchName));
            }
            if (!String.IsNullOrEmpty(searchLastName))
            {
                students = students.Where(s => s.LastName.Contains(searchLastName));
            }
            if (!String.IsNullOrEmpty(searchStudentID))
            {

                students = students.Where(s => s.StudentID.Contains(searchStudentID));
            }

            return View(await students.ToListAsync());

            //return View(await _context.Student.ToListAsync());
        }
        public async Task<IActionResult> Subjects(int? id)
        {

            var student = from s in _context.Student
                          select s;
            student = student.Where(s => s.ID == id);
            string studentId = student.Select(s => s.StudentID).ToString();
            var enrollments = from e in _context.Enrollment
                              select e;
            enrollments = enrollments.Where(e => e.StudentID.Equals(studentId));
            return View(await enrollments.ToListAsync());
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,FirstName,LastName,EnrolmentDate,AcquiredCredits,CurrentSemester,EducationLevel,pic")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudentID,FirstName,LastName,EnrolmentDate,AcquiredCredits,CurrentSemester,EducationLevel,pic")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
