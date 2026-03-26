using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using capapi.Data;

namespace capapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisplaysController : ControllerBase
    {
        private readonly Context _context;

        public DisplaysController(Context context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var x = _context.orderLines.ToList();
            var result = new List<object>();

            foreach (var ol in x)
            {
                var shelf = _context.shelves.Where(s => s.Id == ol.ShelfId).FirstOrDefault();
                var order = _context.orders.Where(o => o.Id == ol.OrderId).FirstOrDefault();

                result.Add(Mappers.DisplayMapper.ToShelfDisplayStateDto(shelf, ol, order));
            }

            return Ok(result);
        }
    }
}
