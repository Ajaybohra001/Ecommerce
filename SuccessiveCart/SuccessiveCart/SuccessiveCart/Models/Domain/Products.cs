using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice {  get; set; }
        public string? ProductPhoto {  get; set; }

        public string ProductDescription {  get; set; }

        public bool IsAvailable { get; set; }
        
        public bool IsTrending { get; set; }

        public DateTime ProductCreatedDate { get; set; }

        public int CateogryId { get; set; }
        [ForeignKey("CateogryId")]
        public Cateogry Cateogries { get; set; }
      
      
       


       



    }
}
