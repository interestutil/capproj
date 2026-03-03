using System.ComponentModel.DataAnnotations.Schema;

namespace capproj.Models
{
    public class OrderLines
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShelfId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        // Navigation back to parent order
        public Order order { get; set; }

        // Navigation to shelf
        public Shelf shelf { get; set; }
    }
}
