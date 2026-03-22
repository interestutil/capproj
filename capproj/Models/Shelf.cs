namespace capproj.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayIndex { get; set; }
        // Navigation to order lines that reference this shelf
        public List<OrderLines> orderLines { get; set; } = new List<OrderLines>();
    }
}
