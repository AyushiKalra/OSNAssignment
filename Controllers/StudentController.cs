using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsnTestApp.Data;
using System.Text.RegularExpressions;

namespace OsnTestApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _context.Student
                           .Include(s => s.Parent)
                           .Include(s => s.Marks)
                           .ToListAsync();
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Student addStudentRequest)
        {
            for (int i = 0; i < addStudentRequest.Marks.Count; i++)
            {
                bool isAbsent = false;
                if (Request.Form[$"Marks[{i}].IsAbsent"].FirstOrDefault() == "on")
                    isAbsent = true;
                addStudentRequest.Marks[i].IsAbsent = isAbsent;
            }
            await _context.Student.AddAsync(addStudentRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var student = await _context.Student
                           .Include(s => s.Parent)
                           .Include(s => s.Marks)
                           .FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                //validation
                return RedirectToAction("Index");
            }
            return await Task.Run(() => View("View", student));
        }

        //Edit
        [HttpPost]
        public async Task<IActionResult> View([Bind("Id,Name,DateOfBirth,Address,Parent,Marks")] Student editStudent)
        {
            var student = await _context.Student
                           .Include(s => s.Parent)
                           .Include(s => s.Marks)
                           .FirstOrDefaultAsync(x => x.Id == editStudent.Id);
            if (student != null)
            {
                student.Name = editStudent.Name;
                student.DateOfBirth = editStudent.DateOfBirth;
                student.Address = editStudent.Address;
                student.Parent = editStudent.Parent;
                student.Marks = editStudent.Marks;

                _context.Student.Update(student);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Student delStudent)
        {
            var student = await _context.Student
                           .Include(s => s.Parent)
                           .Include(s => s.Marks)
                           .FirstOrDefaultAsync(x => x.Id == delStudent.Id);
            if (student != null)
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult SearchByName(string studentName)
        {
            var students = _context.Student
            .Include(s => s.Parent)
            .Where(s => s.Name.Contains(studentName))
            .ToList();

            return View(students);
        }
        [HttpGet]
        public IActionResult SearchByNameAndTerm(string studentName, int term)
        {
            var student = _context.Student
                .Include(s => s.Marks)
                .Where(s => s.Name == studentName && s.Marks.Any(t => t.Term == term)).ToList();

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

    }
}
