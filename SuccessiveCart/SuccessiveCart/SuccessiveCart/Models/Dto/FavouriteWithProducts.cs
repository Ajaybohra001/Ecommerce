using SuccessiveCart.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Dto
{
    public class FavouriteWithProducts
    {
        public Guid FavId { get; set; }
        public string UserId { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public string? ProductPhoto { get; set; }

        public string ProductDescription { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsTrending { get; set; }

        public bool IsFavourite { get; set; }


        public DateTime ProductCreatedDate { get; set; }

        public int CateogryId { get; set; }
  
    }
}
