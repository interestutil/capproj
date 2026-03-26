namespace capapi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool Type { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
