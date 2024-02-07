using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Name is required")]

        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Please enter a valid phone number")]
        public string? UserPhoneNo { get; set; }

        public string? UserRole = "User";
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("UserPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
