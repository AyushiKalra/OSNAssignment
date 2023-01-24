using System.ComponentModel;

namespace OsnTestApp.Data
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("Student Name")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public List<StudentMark> Marks { get; set; }
        public Parent Parent { get; set; }
        public int ParentId { get; set; }
    }
}
