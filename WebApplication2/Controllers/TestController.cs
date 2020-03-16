using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repos.Abstract;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;

        public TestController(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public IActionResult AddClasses()
        {
            var student = _studentRepository.All.FirstOrDefault();
            var classes = _classRepository.All.ToList();

            student.Classes = classes;

            _studentRepository.Save(student);

            return Ok();
        }
    }
}