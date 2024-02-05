using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string UserPhoneNo { get; set; }
        public string UserRole { get; set; } = "User";

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required (ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "Password is not Matched")]
        public string ConfirmPassword { get; set; }

        public bool isActive { get; set; }
    }
}
