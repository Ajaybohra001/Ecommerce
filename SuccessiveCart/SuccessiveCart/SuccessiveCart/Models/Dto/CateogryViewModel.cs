using System.ComponentModel.DataAnnotations;

namespace SuccessiveCart.Models.Dto
{
    public class CateogryViewModel
    {
        [Key]

        public int CateogryId { get; set; }
     public   string CateogryName { get; set; }
    }
}
