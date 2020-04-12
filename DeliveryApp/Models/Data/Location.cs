namespace DeliveryApp.Models.Data
{
    public class Location : BaseEntity
    {
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}