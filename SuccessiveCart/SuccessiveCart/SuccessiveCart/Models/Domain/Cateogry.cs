namespace SuccessiveCart.Models.Domain
{
    public class Cateogry
    {
        public int CateogryId {  get; set; }
        public string CateogryName { get; set; }

        public string CateogryPhoto {  get; set; }

        public ICollection<Products> Products { get; set; }


    }
}
