using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ViewModels
{
    public class SignUpViewModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Mobile number cannot exceed 15 characters.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password confirmation must be between 6 and 100 characters.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }



    }
}
