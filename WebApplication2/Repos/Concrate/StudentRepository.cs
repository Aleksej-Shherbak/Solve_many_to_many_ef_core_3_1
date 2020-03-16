using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repos.Abstract;

namespace WebApplication2.Repos.Concrate
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Student> All => _dbContext.Students;

        public void Save(Student student)
        {
            if (student.Id == 0)
            {
                _dbContext.Students.Add(student);
            }
            else
            {
                _dbContext.Attach(student).State = EntityState.Modified;
                _dbContext.Update(student);
            }

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEntity = _dbContext.Students.Find(id);

            if (dbEntity != null)
            {
                _dbContext.Students.Remove(dbEntity);
                _dbContext.SaveChanges();
            }
        }
    }
}