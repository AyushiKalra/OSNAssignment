using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OsnTestApp.Data;

namespace OsnTestApp.Controllers
{
    public class APIController : Controller
    {
        private readonly DataContext _context;

        public APIController(DataContext context)
        {
            _context = context;
        }

        [Route("api/searchNameTerm")]
        [HttpGet]
        public JsonResult SearchByStudentNameAndTerm(string studentName, int term)
        {
            var student = _context.Student
                .Include(s => s.Marks)
                .Where(s => s.Name == studentName && s.Marks.Any(t => t.Term == term)).ToList();

            return Json(student);
        }
    }
}
