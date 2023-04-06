using ExcelAtUni.Data;
using ExcelAtUni.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelAtUni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studenttRepository;

        public StudentController(IStudentRepository departmentRepository)
        {
            _studenttRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAllDepartments()
        {
            return await _studenttRepository.GetStudents();
        }
    }
}
