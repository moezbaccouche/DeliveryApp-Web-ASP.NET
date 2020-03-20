namespace DeliveryApp.Models.Data
{
    public class Rating : BaseEntity
    {
        public EnumRate Rate { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
    }
}