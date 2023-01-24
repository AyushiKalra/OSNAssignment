using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsnTestApp.Data;

namespace OsnTestApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DataContext dbContext;

        public ReportsController(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Report()
        {
            var students = await dbContext.Student
                           .Where(s => s.Marks.Count(m => m.Mark > 90) >= 3)
                           .Select(s => new
                           {
                               Student = s,
                               Marks = s.Marks
                                .Where(m => m.Mark > 90)
                                .Select(m => new { m.SubjectName, m.Mark })
                           }).ToListAsync();
            return View(students);
        }

        public IActionResult Report1()
        {
            var students = dbContext.Student
                           .Where(s => s.Marks.Count(m => m.Mark > 90) >= 3)
                           .Select(s => new
                           {
                               Student = s,
                               Marks = s.Marks
                                    .Where(m => m.Mark > 90)
                                    .Select(m => new { m.SubjectName, m.Mark })
                           }).ToList();
            return View(students);
        }

        public IActionResult Report2()
        {
            var subjects = dbContext.StudentMark
                           .GroupBy(m => m.SubjectName)
                           .Where(g => g.Count(m => m.Mark > 90) >= 5)
                           .Select(g => new
                           {
                               SubjectName = g.Key,
                               Count = g.Count(m => m.Mark > 90)
                           }).ToList();

            return View(subjects);
        }

        public IActionResult Report3()
        {
            var subjects = dbContext.StudentMark
                           .Where(m => m.Mark < 10)
                           .GroupBy(m => m.SubjectName)
                           .Where(g => g.Count() >= 5)
                           .Select(g => new
                           {
                               SubjectName = g.Key,
                               Count = g.Count(),
                               Students = g.Select(m => m.Student)
                           }).ToList();
            return View(subjects);
        }

        public IActionResult Report4()
        {
            var subjects = dbContext.StudentMark
                            .Where(m => m.Mark == 0 || m.IsAbsent)
                            .GroupBy(m => m.SubjectName)
                            .Select(g => new
                            {
                                SubjectName = g.Key,
                                Count = g.Count(),
                                Students = g.Select(m => m.Student)
                            }).ToList();
            return View(subjects);
        }
    }
}