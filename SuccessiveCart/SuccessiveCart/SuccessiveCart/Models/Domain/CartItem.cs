using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class CartItem
    {
        [Key]
        [ForeignKey("Users")]
        public int CartId { get; set; }
        public int ProductId {  get; set; }
        public int ProductQuantity {  get; set; }

        public ICollection<Products> Products { get; set; }
       
        public virtual Users Users { get; set; }
      
       

    }
}
