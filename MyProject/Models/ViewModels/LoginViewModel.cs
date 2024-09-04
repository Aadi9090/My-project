using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ViewModels
{
    public class LoginViewModel
    {


        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }
    }
}
