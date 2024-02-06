using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Domain
{
    public class FavoriteModel
    {
        [Key]
        public Guid FavId { get; set; }
        public string UserId { get; set; }
        public virtual Users Users { get; set; }

        public int ProductId { get; set; }
        public virtual Products Products { get; set; }
    }
}
