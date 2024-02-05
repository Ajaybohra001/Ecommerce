using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class CartItem
    {
        [Key]
        [Required]
        public Guid CartId { get; set; }
        [Required]
        public int ProductId {  get; set; }
        public int ProductQuantity {  get; set; }

        public virtual Products? Products { get; set; }
       
        public Guid UserId {  get; set; }
        public virtual  Users? Users { get; set; }
      
       

    }
}
