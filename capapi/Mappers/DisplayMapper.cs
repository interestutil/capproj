using capapi.Dtos;
using capapi.Models;

namespace capapi.Mappers
{
    public static class DisplayMapper
    {
        public static ShelfDisplayStateDto ToShelfDisplayStateDto(Shelf shelf, OrderLine orderLine, Order order)
        {
            return new ShelfDisplayStateDto
            {
                DisplayIndex = shelf.DisplayIndex,
                Text = $"{order.Type} #{order.Id} {orderLine.Quantity}"
            };
        }
    }
}
