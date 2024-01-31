using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class CartItem
    {
        [Key]
        [ForeignKey("Users")]

        [Required]
        public int CartId { get; set; }
        [Required]
        public int ProductId {  get; set; }
        public int ProductQuantity {  get; set; }

        public Products Products { get; set; }
       
        public int UserId {  get; set; }
        public  Users Users { get; set; }
      
       

    }
}
