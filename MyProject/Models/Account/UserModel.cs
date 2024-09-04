using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.Account
{
    public class UserModel
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}
