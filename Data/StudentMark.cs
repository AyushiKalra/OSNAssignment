using System.ComponentModel;

namespace OsnTestApp.Data
{
    public class StudentMark
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }
        public int Term { get; set; }
        public int Mark { get; set; }
        public bool IsAbsent { get; set; }

        public Student Student { get; set; }
    }
}
