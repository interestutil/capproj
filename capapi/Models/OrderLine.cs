using System.ComponentModel.DataAnnotations.Schema;

namespace capapi.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShelfId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ShelfId")]
        public Shelf Shelf { get; set; }
    }
}
