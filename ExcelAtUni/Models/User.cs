namespace ExcelAtUni.Models
{
    public class User
    {
        public User()
        {

        }

        public User(int id, string firstname, string lastname, string email, string idNumber, string phone, byte[] passwordHash, byte[] passwordSalt)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            IdNumber = idNumber;
            Phone = phone;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
