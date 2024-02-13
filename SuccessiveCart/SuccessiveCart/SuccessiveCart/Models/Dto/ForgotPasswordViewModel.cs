using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class ForgotPasswordViewModel
    {
        [Required,Display(Name ="Registered Email address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
