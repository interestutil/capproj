namespace capapi.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayIndex { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
