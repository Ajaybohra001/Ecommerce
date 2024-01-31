using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class ProductCateogry
    {
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public string? ProductPhoto { get; set; }

        public string ProductDescription { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsTrending { get; set; }

        public DateTime ProductCreatedDate { get; set; }
      
        public int CateogryId { get; set; }
        public string CateogryName { get; set; }

        public string? CateogryPhoto { get; set; }

    }
}
