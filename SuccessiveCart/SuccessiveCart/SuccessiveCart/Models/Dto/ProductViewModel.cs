using SuccessiveCart.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class ProductViewModel
    {
        [Required]
       public int ProductId {  get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]

        public double ProductPrice { get; set; }
        [Required]

        public IFormFile? ProductPhoto { get; set; }
        [Required]

        public string ProductDescription { get; set; }
      
        public bool IsAvailable { get; set; }

        public bool IsTrending { get; set; }

        public DateTime ProductCreatedDate { get; set; }

        public Cateogry Cateogries { get; set; }
        public int CateogryId { get; set; }

    }
}
