namespace capproj.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool Type { get; set; }
        public bool Status { get; set; }
        // Owned/weak collection of order lines. An OrderLines instance only exists with its parent Order.
        public List<OrderLines> orderLines { get; set; } = new();
    }
}
