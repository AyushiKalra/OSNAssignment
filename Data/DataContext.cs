using Microsoft.EntityFrameworkCore;

namespace OsnTestApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<StudentMark> StudentMark { get; set; }
    }
}
