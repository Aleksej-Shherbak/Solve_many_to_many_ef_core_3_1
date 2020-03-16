using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repos.Abstract;

namespace WebApplication2.Repos.Concrate
{
    public class ClassRepository: IClassRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClassRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Class> All => _dbContext.Classes;

        public void Save(Class @class)
        {
            if (@class.Id == 0)
            {
                _dbContext.Classes.Add(@class);
            }
            else
            {
                _dbContext.Attach(@class).State = EntityState.Modified;
                _dbContext.Update(@class);
            }

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEntity = _dbContext.Classes.Find(id);

            if (dbEntity != null)
            {
                _dbContext.Classes.Remove(dbEntity);
                _dbContext.SaveChanges();
            }
        }
    }
}