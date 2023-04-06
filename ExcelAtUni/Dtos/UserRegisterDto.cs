using System.ComponentModel.DataAnnotations;

namespace ExcelAtUni.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 - 8 characters!")]
        public string Password { get; set; }
    }
}
