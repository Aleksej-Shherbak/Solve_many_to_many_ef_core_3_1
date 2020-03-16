using System.Linq;
using WebApplication2.Models;

namespace WebApplication2.Repos.Abstract
{
    public interface IClassRepository
    {
        IQueryable<Class> All { get; }

        void Save(Class @class);

        void Delete(int id);
    }
}