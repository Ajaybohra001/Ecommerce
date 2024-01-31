using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Domain
{
    public class Cateogry
    {
        [Key]
        [Required]
        public int CateogryId {  get; set; }
        public string CateogryName { get; set; }

        public string? CateogryPhoto {  get; set; }

        public ICollection<Products> Products { get; set; }


    }
}
