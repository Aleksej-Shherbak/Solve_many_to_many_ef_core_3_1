using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApplication2.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public ICollection<ClassStudent> ClassStudents { get; set; }
    
        [NotMapped]
        public IEnumerable<Class> Classes
        {
            get => ClassStudents.Select(r => r.Class);
            set => ClassStudents = value.Select(v => new ClassStudent()
            {
                ClassId = v.Id
            }).ToList();
        }
    }
}