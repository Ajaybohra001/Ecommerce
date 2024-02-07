using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuccessiveCart.Models.Domain
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required")]

        public string ProductName { get; set; }
        [Required(ErrorMessage = "Product price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Product price must be a positive number")]

        public double ProductPrice {  get; set; }

        [Display(Name = "Product Photo")]

        public string? ProductPhoto {  get; set; }

        [Required(ErrorMessage = "Product description is required")]


        public string ProductDescription {  get; set; }
        [Display(Name = "Available")]


        public bool IsAvailable { get; set; }
        [Display(Name = "Trending")]


        public bool IsTrending { get; set; }

        [Display(Name = "Favorite")]

        public bool IsFavourite { get; set; }

        [Display(Name = "Created Date")]

        public DateTime ProductCreatedDate { get; set; }

        public int CateogryId { get; set; }
        [ForeignKey("CateogryId")]
        public Cateogry Cateogries { get; set; }

        
      
      
       


       



    }
}
