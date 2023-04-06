using ExcelAtUni.Models;

namespace ExcelAtUni.Data
{
    public interface IStudentRepository
    {
        Task<IEnumerable<User>> GetStudents();
    }
}
