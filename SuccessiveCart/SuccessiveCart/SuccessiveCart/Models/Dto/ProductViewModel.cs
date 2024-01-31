using SuccessiveCart.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class ProductViewModel
    {
       public int ProductId {  get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public IFormFile? ProductPhoto { get; set; }

        public string ProductDescription { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsTrending { get; set; }

        public DateTime ProductCreatedDate { get; set; }

        public Cateogry Cateogries { get; set; }
        public int CateogryId { get; set; }

    }
}
