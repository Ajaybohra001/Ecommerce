using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Domain
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string UserName { get; set; }

        public long UserPhoneNo {  get; set; }

        public string UserRole {  get; set; }
        public string UserEmail { get; set; }

        public string UserPassword {  get; set; }

        public virtual CartItem CartItems { get; set; }
        
        
        

    }
}
