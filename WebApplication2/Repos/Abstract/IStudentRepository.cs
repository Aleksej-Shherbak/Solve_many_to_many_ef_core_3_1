using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Repos.Abstract
{
    public interface IStudentRepository
    {
        IQueryable<Student> All { get; }

        void Save(Student student);

        void Delete(int id); 
    }
}