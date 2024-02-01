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

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "Password is not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
