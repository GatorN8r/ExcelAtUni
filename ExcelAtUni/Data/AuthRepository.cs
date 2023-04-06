using Dapper;
using ExcelAtUni.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ExcelAtUni.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _dataContext = new DataContext(new ConfigurationBuilder());
        public async Task<User> Login(string username, string password)
        {
            try
            {
                // var user = await _dataContext.Users.FirstOrDefaultAsync(usr => usr.Username == username);
                User? user = null;

                using (var db = new SqlConnection(_dataContext.GetConnection()))
                {
                    var dynParam = new DynamicParameters();
                    dynParam.Add("username", username, DbType.String);

                    var result = await db.QueryFirstAsync<User>("nt_Sel_GetUserByUsername", dynParam, commandType: CommandType.StoredProcedure);


                    if (result != null)

                        user = new User()
                        {
                            Id = result.Id,
                            Firstname = result.Firstname,
                            PasswordHash = result.PasswordHash,
                            PasswordSalt = result.PasswordSalt
                        };
                }

                if (user == null) throw new Exception("User was not found");

                if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {

                throw new Exception("Error Occured while attempting to login..." + e.Message);
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            string retMsg = string.Empty;

            try
            {
                if (user != null || password != null)
                {
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    using (var db = new SqlConnection(_dataContext.GetConnection()))
                    {
                        var dynParam = new DynamicParameters();
                        dynParam.Add("username", user.Firstname);
                        dynParam.Add("passwordHash", user.PasswordHash);
                        dynParam.Add("passwordSalt", user.PasswordSalt);
                        dynParam.Add("retMsg", retMsg, DbType.String, ParameterDirection.Output);

                        await db.ExecuteAsync("nt_Ins_RegisterUser", dynParam, commandType: CommandType.StoredProcedure);

                        retMsg = dynParam.Get<string>("retMsg");

                        if (retMsg == "Successful")
                            return user;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string username)
        {
            bool userExists = false;

            int reslt = 0;

            using (var db = new SqlConnection(_dataContext.GetConnection()))
            {
                var dynParam = new DynamicParameters();

                dynParam.Add("username", username, DbType.String);
                dynParam.Add("userExists", userExists, DbType.Int32);

                reslt = await db.QueryFirstAsync<int>("nt_Sel_VerifyUsers", dynParam, commandType: CommandType.StoredProcedure);

                if (reslt == 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
