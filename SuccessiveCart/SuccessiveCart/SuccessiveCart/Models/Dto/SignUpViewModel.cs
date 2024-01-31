using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class SignUpViewModel
    {
        [Required]
        public string UserName {  get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
        public string UserPhoneNumber {  get; set; }


    }
}
