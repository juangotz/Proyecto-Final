namespace APICODERHOUSE.Models
{
    public class SoldProduct
    {
        public int id { get; set; }
        public string description { get; set; }
        public int stock { get; set; }
        public int idUser { get; set; }
        public int idProduct { get; set; }
    }
}
