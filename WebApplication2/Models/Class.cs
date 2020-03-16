using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication2.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        public ICollection<ClassStudent> ClassStudents { get; set; }

        [NotMapped]
        public IEnumerable<Student> Students
        {
            get => ClassStudents.Select(r => r.Student);
            set => ClassStudents = value.Select(v => new ClassStudent()
            {
                StudentId = v.Id
            }).ToList();
        }
    }
}