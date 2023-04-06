using ExcelAtUni.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace ExcelAtUni.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _dataContext = new DataContext(new ConfigurationBuilder());
        public async Task<IEnumerable<User>> GetStudents()
        {
            try
            {
                using (var db = new SqlConnection(_dataContext.GetConnection()))
                {
                    var students = await db.QueryAsync<User>("eau_Sel_GetAllStudentDetails", commandType: CommandType.StoredProcedure);

                    if (students != null)
                    {
                        return students;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("could not retreive departments:" + ex.Message);
            }
            return null;
        }
    }
}
