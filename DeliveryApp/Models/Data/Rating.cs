namespace DeliveryApp.Models.Data
{
    public class Rating : BaseEntity
    {
        public int IdClient { get; set; }
        public int IdDeliveryMan { get; set; }
        public int Rate { get; set; }
    }
}