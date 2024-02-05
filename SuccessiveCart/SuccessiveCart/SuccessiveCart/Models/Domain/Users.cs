using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class Users : IdentityUser
    {
        [Key]
        public int UserID { get; set; }



        [Required]
        public string? Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? UserPhoneNo {  get; set; }

        public string? UserRole {  get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string UserPassword {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("UserPassword",ErrorMessage ="Password is not Matched")]
        public string ConfirmPassword {  get; set; }

        [DefaultValue(true)]
        public bool isActive {  get; set; }

        
       
        
        
        

    }
}
