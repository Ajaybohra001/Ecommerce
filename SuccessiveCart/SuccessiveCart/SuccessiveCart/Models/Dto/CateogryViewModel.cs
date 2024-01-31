using SuccessiveCart.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class CateogryViewModel
    {
        
     public   string CateogryName { get; set; }
       

        public IFormFile? CateogryPhoto { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
