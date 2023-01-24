using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OsnTestApp.Data
{
    public class Parent
    {
        public int Id { get; set; }
        [DisplayName("Parent Name")]
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<Student> Students { get; set; }
    }
}
