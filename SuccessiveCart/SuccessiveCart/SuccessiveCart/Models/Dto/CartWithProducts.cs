using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class CartWithProducts
    {
       
        public Guid CartId { get; set; }
       
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }

        
       
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }
        public string? ProductPhoto { get; set; }

        public string ProductDescription { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsTrending { get; set; }

        public DateTime ProductCreatedDate { get; set; }
    }
}
