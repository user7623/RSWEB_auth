using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspProekt.Model;
using aspProekt.Models;
using Microsoft.AspNetCore.Authorization;

namespace aspProekt.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AspProektContext _context;

        public CoursesController(AspProektContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchSemestar, string searchString, string searchPrograma, string searchTeacher)
        {

            var courses = from m in _context.Course
                          select m;
            var teachers = from v in _context.Teacher
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchPrograma))
            {
                courses = courses.Where(s => s.Programe.Contains(searchPrograma));
            }
            if (!String.IsNullOrEmpty(searchSemestar))
            {
                int x = 0;

                Int32.TryParse(searchSemestar, out x);
                courses = courses.Where(s => s.Semester == x);
            }
            if (!String.IsNullOrEmpty(searchTeacher))
            {
                int x = 0;

                Int32.TryParse(searchTeacher, out x);
                courses = courses.Where(s => s.FirstTeacherID == x || s.SecondTeacherID == x);

            }


            return View(await courses.ToListAsync());
            //return View(await _context.Course.ToListAsync());
        }


        //// GET: Courses
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Course.ToListAsync());
        //}

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Credits,Semester,Programe,EducationLevel,FirstTeacherID,SecondTeacherID")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Credits,Semester,Programe,EducationLevel,FirstTeacherID,SecondTeacherID")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
        [HttpGet]
        private async Task<IActionResult> showStudents()
        {
            var students = from m in _context.Student
                           select m;
            return View(await students.ToListAsync());
        }
    }
}
